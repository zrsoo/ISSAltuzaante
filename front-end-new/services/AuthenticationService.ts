import {AuthenticationController} from "../controllers/AuthenticationController";

export class AuthenticationService {
    static async getAuthenticatedUser() {
        if (localStorage.getItem('token') != null) {
            try {
                const user = await AuthenticationController.getUser(localStorage.getItem('token'));
                if (user === null)
                    return "not-authenticated"
                return user
            } catch (error) {
                return "Error when trying to get authenticated user";
            }
        }
    }
}