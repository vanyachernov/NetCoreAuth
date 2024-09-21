import {base} from "../../../shared/base.ts";
import axios from "axios";

export const apiClient = axios.create({
    baseURL: base.baseUrl.component,
    headers: {
        'Content-Type': 'application/json'
    }
});

const logout = () => {
    localStorage.removeItem("panel"); 
    window.location.href = "/login";
};

apiClient.interceptors.response.use(
    (response) => {
        return response;
    },
    (error) => {
        if (error.response && error.response.status === 403) {
            console.log("Пользователь заблокирован или удалён. Выполняется выход из системы.");
            logout();
        }
        return Promise.reject(error);
    }
);