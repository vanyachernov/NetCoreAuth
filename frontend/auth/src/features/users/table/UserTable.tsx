import React from "react";
import {
    Table,
    Thead,
    Tbody,
    Tr,
    Th,
    Td,
    Checkbox
} from "@chakra-ui/react";
import { UserResponse } from "../../auth/models/UserResponse.ts";

interface UserTableProps {
    data: UserResponse[];
    selectedRows: string[];
    handleSelectRow: (id: string) => void;
    handleSelectAll: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

const UserTable = ({ data, selectedRows, handleSelectRow, handleSelectAll }: UserTableProps) => {
    
    return (
        <Table variant="simple">
            <Thead>
                <Tr>
                    <Th>
                        <Checkbox
                            isChecked={selectedRows.length === data.length}
                            isIndeterminate={
                                selectedRows.length > 0 && selectedRows.length < data.length
                            }
                            onChange={handleSelectAll}
                        />
                    </Th>
                    <Th color="white">Name / Position</Th>
                    <Th color="white">Email</Th>
                    <Th color="white">Register At</Th>
                    <Th color="white">Last Login</Th>
                    <Th color="white">Status</Th>
                </Tr>
            </Thead>
            <Tbody color="white">
                {data.map((row: UserResponse) => (
                    <Tr key={row.id}>
                        <Td>
                            <Checkbox
                                isChecked={selectedRows.includes(row.id)}
                                onChange={() => handleSelectRow(row.id)}
                            />
                        </Td>
                        <Td>{row.fullName.firstName} {row.fullName.lastName}</Td>
                        <Td>{row.email.value}</Td>
                        <Td>{new Date(row.registerAt.date).toLocaleString()}</Td>
                        <Td>{new Date(row.lastAuthAt.date).toLocaleString()}</Td>
                        <Td>{row.status.value ? "Blocked" : "Active"}</Td>
                    </Tr>
                ))}
            </Tbody>
        </Table>
    );
};

export default UserTable;
