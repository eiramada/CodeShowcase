import apiService from "./apiService";

const getUserStatistics = () => {
  return apiService.get("userStatistics");
};

export const userStatisticsService = {
  getUserStatistics,
};
