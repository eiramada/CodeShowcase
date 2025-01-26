import React, { useCallback, useEffect, useState } from "react";
import { userStatisticsService } from "../services/userStatisticsService";
import { formattedDate } from "../utils/helpers";

const AdminView = () => {
  const adminId = sessionStorage.getItem("userId");
  const [userStatistics, setUserStatistics] = useState(null);

  const fetchData = useCallback(async () => {
    try {
      const response = await userStatisticsService.getUserStatistics(adminId);
      setUserStatistics(response.data);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  }, [adminId]);

  useEffect(() => {
    fetchData();
  }, [fetchData]);

  return (
    <>
      <div>
        <h2>Admin Dashboard</h2>
        <p>Welcome to the admin dashboard!</p>
      </div>
      <div className="card">
        <table className="table mt-3">
          <thead>
            <tr>
              <th>Name</th>
              <th>Joined</th>
              <th>Total storage levels</th>
              <th>Total items</th>
              <th>Items with description</th>
              <th>Items with image</th>
            </tr>
          </thead>
          <tbody>
            {userStatistics?.map((data) => (
              <tr key={data.userId}>
                <td>{data.userName}</td>
                <td>{formattedDate(data.userJoined)}</td>
                <td>{data.totalStorageLevels}</td>
                <td>{data.totalItems}</td>
                <td>{data.itemsWithDescription}</td>
                <td>{data.itemsWithImage}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </>
  );
};

export default AdminView;
