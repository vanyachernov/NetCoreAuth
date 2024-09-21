import { useNavigate } from "react-router-dom";
import AuthForm from "../../../components/AuthForm.tsx";
import { CreateUserRequest } from "../../auth/models/CreateUserRequest.ts";
import {Register} from "../services/authService.ts";
import {routes} from "../../../shared/routes.ts";
import {useToast} from "@chakra-ui/react";
import {ShowToast} from "../../../utils/toast.ts";
import axios from "axios";
import {APIError} from "../services/userService.ts";
import {AuthenticateUserRequest} from "../../auth/models/AuthenticateUserRequest.ts";

const RegisterActions = () => {
    const navigate = useNavigate();
    const toast = useToast();

    const handleRegister = async (request: AuthenticateUserRequest | CreateUserRequest) => {
        if ("fullNameDto" in request) {
            try {
                await Register(request as CreateUserRequest);
                navigate(routes.login.path);
            } catch (error) {
                let errorMessage = "Не удалось зарегистрироваться. Попробуйте позже!";

                if (axios.isAxiosError(error)) {
                    const errors: APIError[] | undefined = error.response?.data?.errors;

                    if (Array.isArray(errors)) {
                        errorMessage = errors
                            .map((err: APIError) => err.errorMessage || "Произошла ошибка")
                            .join(", ");
                    }
                }

                ShowToast(toast, "Ошибка регистрации", errorMessage, "error", 5000, "bottom-right");
            }
        } else {
            ShowToast(toast, "Ошибка", "Неверный формат данных.", "error", 5000, "bottom-right");
        }
    };

    return (
        <AuthForm
            title="Регистрация"
            buttonText="Зарегистрироваться"
            onSubmit={handleRegister}
            isRegister={true}
        />
    );
};

export default RegisterActions;
