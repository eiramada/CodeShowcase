import React from "react";
import AdminView from "../components/AdminView";
import UserView from "../components/UserView";
import { StorageProvider } from "../contexts/StorageLevelContext";

function Dashboard() {
  const isAdmin = sessionStorage.getItem("isAdmin") === "true";

  return (
    <div>
      {isAdmin ? (
        <AdminView />
      ) : (
        <StorageProvider>
          <UserView />
        </StorageProvider>
      )}
    </div>
  );
}

export default Dashboard;
