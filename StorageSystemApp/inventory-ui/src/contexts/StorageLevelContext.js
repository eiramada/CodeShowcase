import React, { createContext, useCallback, useState, useEffect } from "react";
import { storageLevelService } from "../services/storageLevelService";

export const StorageContext = createContext();

export const StorageProvider = ({ children }) => {
  const [storageLevels, setStorageLevels] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const fetchStorageLevels = useCallback(async () => {
    setLoading(true);
    try {
      const response = await storageLevelService.getAllStorageLevelsForUser();
      setStorageLevels(response.data);
      setError(null);
    } catch (error) {
      setError(error);
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetchStorageLevels();
  }, [fetchStorageLevels]);

  return (
    <StorageContext.Provider
      value={{ storageLevels, loading, error, fetchStorageLevels }}
    >
      {children}
    </StorageContext.Provider>
  );
};

export default StorageProvider;
