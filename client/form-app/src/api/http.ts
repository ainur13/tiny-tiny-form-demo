import axios from "axios";
import notify from "notify-zh";

const API_BASE_URL = "http://localhost:5166/api"

const api = axios.create({
  baseURL: API_BASE_URL,
  withCredentials: false,
})

api.interceptors.response.use(
  response => response,
  error => {
    notify.error(error?.response?.data?.message || "Network error");
    return Promise.reject(error);
  }
);

export default api;