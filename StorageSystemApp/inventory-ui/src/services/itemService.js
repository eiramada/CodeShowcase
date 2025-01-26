import apiService from "./apiService";

const createItem = (itemData) => {
  return apiService.post("items", itemData);
};

const getItem = (id) => {
  return apiService.get(`items/${id}`);
};

const updateItem = (id, itemData) => {
  return apiService.put(`items/${id}`, itemData);
};

const searchItems = (searchTerm) => {
  return apiService.get(
    `items/search?search=${encodeURIComponent(searchTerm)}`
  );
};

export const itemService = {
  createItem,
  getItem,
  updateItem,
  searchItems,
};
