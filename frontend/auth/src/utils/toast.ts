import { useToast } from "@chakra-ui/react";

export const ShowToast = (
    toast: ReturnType<typeof useToast>,
    title: string,
    description: string,
    status: "success" | "error" | "info" | "warning",
    duration: number = 5000,
    position: "top-right" | "bottom-right" | "top-left" | "bottom-left" | "top" | "bottom" = "bottom-right"
) => {
    toast({
        title,
        description,
        status,
        duration,
        isClosable: true,
        position,
    });
};
