export interface UserResponse {
    id: string;
    fullName: {
        firstName: string;
        lastName: string;
    }
    email: {
        value: string;
    }
    registerAt: {
        date: Date;
    }
    lastAuthAt: {
        date: Date;
    }
    status: {
        value: boolean;
    }
}