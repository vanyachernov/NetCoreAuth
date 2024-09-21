export interface CreateUserRequest {
    fullNameDto: {
        firstName: string;
        lastName: string;
    }
    email: string;
    password: string;
}