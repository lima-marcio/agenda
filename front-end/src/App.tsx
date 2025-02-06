// import { useState } from "react";
// import reactLogo from "./assets/react.svg";
// import viteLogo from "/vite.svg";
import "./App.css";
import UsersList from "./User/UsersList";

function App() {
  // const [count, setCount] = useState(0);

  return (
    <div className="container">
      <h1>Usuários</h1>
      <UsersList />
    </div>
  );
}

export default App;
