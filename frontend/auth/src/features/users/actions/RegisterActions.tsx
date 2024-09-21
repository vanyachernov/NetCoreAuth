import { useNavigate } from "react-router-dom";
import AuthForm from "../../../components/AuthForm.tsx";
import { CreateUserRequest } from "../../auth/models/CreateUserRequest.ts";
import {Register} from "../services/authService.ts";
import {routes} from "../../../shared/routes.ts";
import {useToast} from "@chakra-ui/react";
import {ShowToast} from "../../../utils/toast.ts";

const RegisterActions = () => {
    const navigate = useNavigate();
    const toast = useToast();

    const handleRegister = async (request: CreateUserRequest) => {
        try {
            await Register(request);
            navigate(routes.login.path);
        } catch (error : Response) {
            const errors = error?.response?.data?.errors;
            let errorMessage = "Не удалось зарегистрироваться. Попробуйте позже!";

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
            title="Регистрация"
            buttonText="Зарегистрироваться"
            onSubmit={handleRegister}
            isRegister={true}
        />
    );
};

export default RegisterActions;
