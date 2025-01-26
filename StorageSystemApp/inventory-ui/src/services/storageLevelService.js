import apiService from "./apiService";

const getAllStorageLevelsForUser = () => {
  return apiService.get(`storageLevels`);
};

const createStorageLevel = (storageData) => {
  return apiService.post("storageLevels", storageData);
};

export const storageLevelService = {
  getAllStorageLevelsForUser,
  createStorageLevel,
};
