import {base} from "../../../shared/base.ts";
import {UserResponse} from "../../auth/models/UserResponse.ts";
import {ChangeUserStatusRequest} from "../../auth/models/ChangeUserStatusRequest.ts";
import {jwtDecode, JwtPayload} from "jwt-decode";
import {apiClient} from "./baseService.ts";

export interface CustomJwtPayload extends JwtPayload {
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"?: string;
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"?: string;
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"?: string;
}

export interface APIError {
    errorMessage: string;
}

const GetUserData = () => {
    const userData = localStorage.getItem("panel");
    return userData ? JSON.parse(userData) : null;
}

export const GetCurrentUserId = (): string | null => {
    const token = localStorage.getItem("panel");

    if (token) {
        try {
            const decoded = jwtDecode<CustomJwtPayload>(token);

            return decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"] || null;
        } catch (error) {
            console.error("Ошибка при декодировании токена:", error);
            return null;
        }
    }

    return null;
}

export const FetchUsers = async (): Promise<UserResponse[]> => {
    try {
        const user = GetUserData();
        const userId = GetCurrentUserId();

        if (!user) {
            throw new Error("No valid user token found");
        }

        const response = await apiClient.get<{ result: UserResponse[] }>(
            `${base.baseUrl.component}/users`,
            {
                headers: {
                    Authorization: `Bearer ${user}`,
                    UserId: userId
                }
            }
        );

        if (!response.data.result) {
            throw new Error("No user data");
        }

        return response.data.result;
    } catch (exception) {
        console.error(exception);
        throw exception;
    }
};

export const ChangeUsersStatus = async (status: boolean, ids : string[]) => {
    const user = GetUserData();
    const userId = GetCurrentUserId();
    
    if (!user) {
        throw new Error("No valid user token found");
    }
    
    for (const id of ids) {
        const requestBody: ChangeUserStatusRequest = {
            status: {
                value: status
            }
        };
        
        await apiClient.post(
            `${base.baseUrl.component}/users/${id}/isDeleted`, 
            requestBody,
            {
                headers: {
                    Authorization: `Bearer ${user}`,
                    UserId: userId,
                    "Content-Type": 'application/json',
                }
            }
        );
    }
};

export const DeleteUsers = async (ids : string[]) => {
    const user = GetUserData();
    const userId = GetCurrentUserId();

    if (!user) {
        throw new Error("No valid user token found");
    }

    for (const id of ids) {
        await apiClient.delete(
            `${base.baseUrl.component}/users/${id}`,
            {
                headers: {
                    Authorization: `Bearer ${user}`,
                    UserId: userId,
                    "Content-Type": 'application/json',
                }
            }
        );
    }
};