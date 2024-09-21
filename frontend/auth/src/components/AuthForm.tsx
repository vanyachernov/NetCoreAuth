import React, { useState } from "react";
import { Box, Button, Input, FormControl, FormLabel, Heading } from "@chakra-ui/react";
import {AuthenticateUserRequest} from "../features/auth/models/AuthenticateUserRequest.ts";
import {CreateUserRequest} from "../features/auth/models/CreateUserRequest.ts";

interface AuthFormProps {
    title: string;
    buttonText: string;
    onSubmit: (formData: AuthenticateUserRequest | CreateUserRequest) => void;
    isRegister?: boolean;
}

const AuthForm = ({ title, buttonText, onSubmit, isRegister = false }: AuthFormProps) => {
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        if (isRegister) {
            onSubmit({
                fullNameDto: { firstName, lastName },
                email,
                password
            });
        } else {
            onSubmit({ email, password });
        }
    };

    return (
        <Box width="300px" mx="auto" mt="100px" p="5" borderWidth="1px" borderRadius="lg">
            <Heading size="lg" mb="6">{title}</Heading>
            <form onSubmit={handleSubmit}>
                {isRegister && (
                    <>
                        <FormControl mb="4">
                            <FormLabel>Имя</FormLabel>
                            <Input
                                type="text"
                                value={firstName}
                                onChange={(e) => setFirstName(e.target.value)}
                                required
                            />
                        </FormControl>
                        <FormControl mb="4">
                            <FormLabel>Фамилия</FormLabel>
                            <Input
                                type="text"
                                value={lastName}
                                onChange={(e) => setLastName(e.target.value)}
                                required
                            />
                        </FormControl>
                    </>
                )}
                <FormControl mb="4">
                    <FormLabel>Почта</FormLabel>
                    <Input
                        type="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </FormControl>
                <FormControl mb="4">
                    <FormLabel>Пароль</FormLabel>
                    <Input
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </FormControl>
                <Button colorScheme="green" type="submit" width="full">
                    {buttonText}
                </Button>
            </form>
        </Box>
    );
};

export default AuthForm;
