import React from "react";
import LoginForm from "../components/LoginForm";
import { Link } from "react-router-dom";

function Login() {
  return (
    <div>
      <h1 className="mb-3">Login</h1>
      <LoginForm /> <br />
      <Link to="/register" className="btn btn-secondary">
            New user? Register
        </Link>
    </div>
  );
}

export default Login;
