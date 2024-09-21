import { useNavigate } from "react-router-dom";
import AuthForm from "../../../components/AuthForm.tsx";
import {Authenticate} from "../services/authService.ts";
import { AuthenticateUserRequest } from "../../auth/models/AuthenticateUserRequest.ts";
import {ShowToast} from "../../../utils/toast.ts";
import {useToast} from "@chakra-ui/react";

const LoginActions = () => {
    const navigate = useNavigate();
    const toast = useToast();

    const handleLogin = async (request: AuthenticateUserRequest) => {
        try {
            const fetchedAuthenticatedUser = await Authenticate(request);

            if (fetchedAuthenticatedUser)
            {
                window.location.reload();
            }
        } catch (error : Response) {
            const errors = error?.response?.data?.errors;
            let errorMessage = "Не удалось авторизироваться. Попробуйте позже!";

            if (Array.isArray(errors)) {
                errorMessage = errors
                    .map((err: any) => err.errorMessage || "Произошла ошибка")
                    .join(", ");
            }

            ShowToast(toast, "Ошибка регистрации", errorMessage, "error", 5000, "bottom-right");
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
