import React, {useEffect, useRef, useState} from "react";
import {
    Heading,
    Box,
    Flex,
    AlertDialog,
    AlertDialogContent,
    AlertDialogOverlay,
    AlertDialogBody,
    AlertDialogFooter,
    Button,
    AlertDialogHeader, useDisclosure, useToast
} from "@chakra-ui/react";
import UserTable from "../features/users/table/UserTable";
import UserActions from "../features/users/actions/UserActions";
import {
    ChangeUsersStatus, DeleteUsers,
    FetchUsers,
    GetCurrentUserId
} from "../features/users/services/userService.ts";
import {UserResponse} from "../features/auth/models/UserResponse.ts";
import {ShowToast} from "../utils/toast.ts";

const UsersPage = () => {
    const [users, setUsers] = useState<UserResponse[]>([])
    const [selectedRows, setSelectedRows] = useState<string[]>([]);
    const { isOpen, onOpen, onClose } = useDisclosure();
    const cancelRef = useRef<HTMLButtonElement>(null);
    const toast = useToast();
    

    useEffect(() => {
        const loadUsers = async () => {
            try {
                const fetchUsers = await FetchUsers();
                setUsers(fetchUsers);
            } catch (error) {
                console.error("Failed to fetch users:", error);
            }
        };

        loadUsers();
    }, []);
    
    const handleSelectRow = (id: string) => {
        setSelectedRows((prevSelected) =>
            prevSelected.includes(id)
                ? prevSelected.filter((rowId) => rowId !== id)
                : [...prevSelected, id]
        );
    };

    const handleSelectAll = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.checked) {
            setSelectedRows(users.map((user) => user.id));
        } else {
            setSelectedRows([]);
        }
    };

    const handleBlock = async () => {
        await ChangeUsersStatus(true, selectedRows);
        const updatedUsers = await FetchUsers();
        setUsers(updatedUsers);
        setSelectedRows([]);

        const user = GetCurrentUserId();
        
        if (user && selectedRows.includes(user))
        {
            handleLogout();
        }
    };

    const handleUnblock = async () => {
        await ChangeUsersStatus(false, selectedRows);
        const updatedUsers = await FetchUsers();
        setUsers(updatedUsers);
        setSelectedRows([]);
    };

    const handleDelete = async () => {
        try {
            await DeleteUsers(selectedRows);

            const user = GetCurrentUserId();

            if (user && selectedRows.includes(user)) {
                handleLogout();
                return;
            }

            const updatedUsers = await FetchUsers();
            setUsers(updatedUsers);
            setSelectedRows([]);
            onClose();

            ShowToast(toast, "Пользователи удалены", "Выбранные пользователи были успешно удалены.", "success");
        } catch (error) {
            const errorMessage = (error as Error).message || "Неизвестная ошибка";
            ShowToast(toast, "Ошибка удаления", `Не удалось удалить выбранных пользователей. ${errorMessage}.`, "error");
        }
    };
    
    const handleLogout = () => {
        localStorage.removeItem("panel");
        window.location.reload();
    };

    return (
        <>
            <Heading textAlign="center" mt="40px" color="green.600">Пользователи</Heading>
            <Flex direction="column" align="center" mt="20px">
                <UserActions
                    onBlock={handleBlock}
                    onUnblock={handleUnblock}
                    onDelete={onOpen}
                    isDisabled={selectedRows.length === 0}
                />
                <Box bgColor="green.600" p={10} maxWidth="1200px" width="100%">
                    <UserTable
                        data={users}
                        selectedRows={selectedRows}
                        handleSelectRow={handleSelectRow}
                        handleSelectAll={handleSelectAll}
                    />
                </Box>
            </Flex>

            <AlertDialog
                isOpen={isOpen}
                leastDestructiveRef={cancelRef}
                onClose={onClose}
                isCentered
            >
                <AlertDialogOverlay>
                    <AlertDialogContent>
                        <AlertDialogHeader fontSize='lg' fontWeight='bold'>
                            Удалить пользователей
                        </AlertDialogHeader>

                        <AlertDialogBody>
                            Вы уверены? Это действие нельзя отменить.
                        </AlertDialogBody>

                        <AlertDialogFooter>
                            <Button ref={cancelRef} onClick={onClose}>
                                Отмена
                            </Button>
                            <Button colorScheme='red' onClick={handleDelete} ml={3}>
                                Удалить
                            </Button>
                        </AlertDialogFooter>
                    </AlertDialogContent>
                </AlertDialogOverlay>
            </AlertDialog>
        </>
    );
};

export default UsersPage;
