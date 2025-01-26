import React, { useState } from "react";
import { userService } from "../services/userService";
import { useNavigate } from "react-router-dom";

const UserForm = ({ type }) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (event) => {
    event.preventDefault();
    setError("");
    try {
      if (type === "login") {
        const response = await userService.login(username, password);
        if (response) {
          navigate("/dashboard");
        } else {
          setError("No response returned");
        }
      } else if (type === "register") {
        const response = await userService.register(username, password);
        if (response) {
          navigate("/dashboard");
        } else {
          setError("No response returned");
        }
      }
    } catch (err) {
      setError("Failed to " + type + ": " + err.message);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="container mt-3">
      <div className="row">
        <div className="col-lg-3 col-md-6">
          <div className="form-group mb-3">
            <input
              className="form-control"
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              placeholder="Username"
              required
            />
          </div>
          <div className="form-group mb-3">
            <input
              className="form-control"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              placeholder="Password"
              required
            />
          </div>
          {error && (
            <p className="alert alert-danger">{JSON.stringify(error)}</p>
          )}
          <button type="submit" className="btn btn-primary">
            {type === "login" ? "Login" : "Register"}
          </button>
        </div>
      </div>
    </form>
  );
};

export default UserForm;
