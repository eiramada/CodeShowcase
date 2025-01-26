import "bootstrap/dist/css/bootstrap.min.css";
import React from "react";
import { Navigate, Route, Routes, useNavigate } from "react-router-dom";
import Dashboard from "./pages/Dashboard";
import Login from "./pages/Login";
import Register from "./pages/Register";

function App() {
  const userId = sessionStorage.getItem("userId");
  const navigate = useNavigate();

  function logOut() {
    sessionStorage.clear();
    navigate("");
  }

  const LogoutButton = () => (
    <div className="d-flex justify-content-end">
      <button onClick={logOut} className="btn btn-light">
        Log Out
      </button>
    </div>
  );

  const redirectTo = (path) => <Navigate to={path} />;

  const authRoutes = (
    <>
      <Route path="" element={redirectTo("/dashboard")} />
      <Route path="/login" element={redirectTo("/dashboard")} />
      <Route path="/register" element={redirectTo("/dashboard")} />
      <Route path="/dashboard" element={<Dashboard />} />
    </>
  );

  const guestRoutes = (
    <>
      <Route path="" element={redirectTo("/login")} />
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />
      <Route path="/dashboard" element={redirectTo("/login")} />
    </>
  );

  return (
    <div className="container mt-4">
      {userId ? <LogoutButton /> : null}
      <Routes>{userId ? authRoutes : guestRoutes}</Routes>
    </div>
  );
}

export default App;
