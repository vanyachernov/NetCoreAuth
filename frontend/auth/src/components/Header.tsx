import { Box, Button, Flex, Text, Link } from '@chakra-ui/react';
import { routes } from "../shared/routes.ts";
import {useNavigate} from 'react-router-dom';
import { useEffect, useState } from "react";
import {jwtDecode} from "jwt-decode";
import { Link as RouterLink } from "react-router-dom";
import {CustomJwtPayload} from "../features/users/services/userService.ts";

interface HeaderProps {
    isAuthenticated: boolean;
}

const Header = ({ isAuthenticated }: HeaderProps) => {
    const navigate = useNavigate();
    const [userName, setUserName] = useState<string | undefined>();

    useEffect(() => {
        const user = localStorage.getItem("panel");
        
        if (user) {
            const decoded = jwtDecode<CustomJwtPayload>(user);
            const firstName = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
            const lastName = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"];
            
            setUserName(`${firstName} ${lastName}`);
        }
    }, [isAuthenticated]);

    const handleLogout = () => {
        const storage = localStorage.getItem("panel");
        
        if (storage)
        {
            localStorage.removeItem("panel");
            window.location.reload();
        }
    };

    return (
        <Box bg="green.600" p={4} color="white">
            <Flex align="center" justify="space-between">
                <Flex justify="space-between" align="center" gap="10">
                    <Text fontSize="xl" color="white" fontWeight="bold">Admin Panel</Text>

                    {isAuthenticated ? (
                        <Link
                            as={RouterLink}
                            to={routes.users.path} 
                            color="blue.200"
                            fontWeight="semibold"
                            fontSize="xl"
                        >
                            Пользователи
                        </Link>
                    ) : null}
                </Flex>
                <Flex>
                    {isAuthenticated ? (
                        <Flex align="center" gap={5}>
                            <Text mr={4}>Привет, {userName}</Text>
                            <Button colorScheme="blue" onClick={handleLogout}>Выйти</Button>
                        </Flex>
                    ) : (
                        <>
                            <Button onClick={() => navigate(routes.login.path)} colorScheme="blue" mr={4}>Войти</Button>
                            <Button onClick={() => navigate(routes.register.path)} colorScheme="blue">Регистрация</Button>
                        </>
                    )}
                </Flex>
            </Flex>
        </Box>
    );
};

export default Header;
