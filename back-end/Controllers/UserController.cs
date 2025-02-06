using Microsoft.AspNetCore.Mvc;
using Agenda.Data;
using Agenda.DTOs;
using Microsoft.EntityFrameworkCore;
using Agenda.Entities;
using BCrypt.Net;

namespace Agenda.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
      var users = await _context.Users
          .Select(u => new UserDto
          {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email
          })
          .ToListAsync();

      return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
      var user = await _context.Users.FindAsync(id);
      if (user == null)
      {
        return NotFound();
      }

      var userDto = new UserDto
      {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email
      };

      return Ok(userDto);
    }

    [HttpPost]
    public async Task<ActionResult<UserCreateDto>> CreateUser(UserCreateDto userDto)
    {
      var user = new User
      {
        Name = userDto.Name,
        Email = userDto.Email,
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
      };

      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      userDto.Id = user.Id;

      return CreatedAtAction(nameof(GetUser), new { id = user.Id }, userDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
    {
      var user = await _context.Users.FindAsync(id);
      if (user == null)
      {
        return NotFound();
      }

      user.Name = userDto.Name;
      user.Email = userDto.Email;

      await _context.SaveChangesAsync();

      return NoContent();
    }

    // 5️⃣ DELETAR UM USUÁRIO PELO ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
      var user = await _context.Users.FindAsync(id);
      if (user == null)
      {
        return NotFound();
      }

      _context.Users.Remove(user);
      await _context.SaveChangesAsync();

      return NoContent();
    }
  }
}
