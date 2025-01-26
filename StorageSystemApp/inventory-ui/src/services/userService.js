import apiService from "./apiService";

const register = (username, password) => {
  return apiService.post("users/register", {
    username,
    password,
  });
};

const login = async (username, password) => {
  try {
    const response = await apiService.post("users/login", {
      username,
      password,
    });
    if (response.data && response.data.token) {
      sessionStorage.setItem("token", response.data.token);

      sessionStorage.setItem("userId", response.data.userId);
      sessionStorage.setItem("isAdmin", response.data.isAdmin);
    }
    return response.data;
  } catch (error) {
    console.error("Login error:", error);
    throw error;
  }
};

export const userService = {
  register,
  login,
};
