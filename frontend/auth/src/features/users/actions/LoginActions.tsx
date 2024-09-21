import AuthForm from "../../../components/AuthForm.tsx";
import {Authenticate} from "../services/authService.ts";
import { AuthenticateUserRequest } from "../../auth/models/AuthenticateUserRequest.ts";
import {ShowToast} from "../../../utils/toast.ts";
import {useToast} from "@chakra-ui/react";
import axios from "axios";
import {APIError} from "../services/userService.ts";

const LoginActions = () => {
    const toast = useToast();

    const handleLogin = async (request: AuthenticateUserRequest) => {
        try {
            const fetchedAuthenticatedUser = await Authenticate(request);

            if (fetchedAuthenticatedUser)
            {
                window.location.reload();
            }
        } catch (error : unknown) {
            let errorMessage = "Не удалось авторизироваться. Попробуйте позже!";

            if (axios.isAxiosError(error)) {
                const errors: APIError[] | undefined = error.response?.data?.errors;

                if (Array.isArray(errors)) {
                    errorMessage = errors
                        .map((err: APIError) => err.errorMessage || "Произошла ошибка")
                        .join(", ");
                }
            }

            ShowToast(toast, "Ошибка авторизации", errorMessage, "error");
        }
    };

    return (
        <AuthForm
            title="Авторизация"
            buttonText="Войти"
            onSubmit={handleLogin}
            isRegister={false}
        />
    );
};

export default LoginActions;
