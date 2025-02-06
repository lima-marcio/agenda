using Agenda.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Adicione seus DbSets (tabelas) aqui, por exemplo:
    public DbSet<User> Users { get; set; }
  }
}
