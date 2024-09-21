import {
    Box,
    Tooltip,
    IconButton,
    Button
} from "@chakra-ui/react";
import { LockIcon, UnlockIcon, DeleteIcon } from "@chakra-ui/icons";

interface UserActionsProps {
    onBlock: () => void;
    onUnblock: () => void;
    onDelete: () => void;
    isDisabled: boolean;
}

const UserActions = ({ onBlock, onUnblock, onDelete, isDisabled }: UserActionsProps) => {
    return (
        <Box mb="4" textAlign="left" width="100%" maxWidth="1200px">
            <Tooltip>
                <Button
                    leftIcon={<LockIcon />}
                    colorScheme="teal"
                    mr="4"
                    onClick={onBlock}
                    isDisabled={isDisabled}
                >
                    Block
                </Button>
            </Tooltip>
            <Tooltip label="Разблокировать">
                <IconButton
                    aria-label="Unblock"
                    icon={<UnlockIcon />}
                    colorScheme="teal"
                    mr="4"
                    onClick={onUnblock}
                    isDisabled={isDisabled}
                />
            </Tooltip>
            <Tooltip label="Удалить">
                <IconButton
                    aria-label="Delete"
                    icon={<DeleteIcon />}
                    colorScheme="red"
                    onClick={onDelete}
                    isDisabled={isDisabled}
                />
            </Tooltip>
        </Box>
    );
};

export default UserActions;
