import {base} from '../../../shared/base'
import axios from "axios";
import {AuthenticateUserRequest} from "../../auth/models/AuthenticateUserRequest.ts";
import {CreateUserRequest} from "../../auth/models/CreateUserRequest.ts";

export const Authenticate = async (request: AuthenticateUserRequest) => {
    try {
        const responseAuthenticatedUser = await axios.post(
            `${base.baseUrl.component}/accounts/authenticate`, 
            request);
        
        if (responseAuthenticatedUser.data.result)
        {
            localStorage.setItem("panel", JSON.stringify(responseAuthenticatedUser.data.result.token));
        }
        
        return responseAuthenticatedUser;
    } catch (exception) {
        console.error(exception);
        throw exception;
    }
}

export const Register = async (request: CreateUserRequest) => {
    try {
        return await axios.post(
            `${base.baseUrl.component}/accounts/register`,
            request);
    } catch (exception) {
        console.error(exception);
        throw exception;
    }
}
