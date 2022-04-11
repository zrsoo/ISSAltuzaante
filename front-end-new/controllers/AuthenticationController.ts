export class AuthenticationController {
    static async register(data): Promise<string> {
        const response = await fetch("https://localhost:7161/register", {
            method: "POST",
            mode: "cors",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });
        if (response.status === 200) {
            return await response.json();
        }
        else {
            var result = "An error occured! ";
            var error = await response.json();
            error = JSON.stringify(error);
            return result + error;
        }
    }

    static async getUser(token) : Promise<any>{
        console.log("TOKEN: " + token)
        return await fetch("https://localhost:7161/get-authenticated-user", {
            method: "GET",
            mode: "cors",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + token
            }
        })
    }

    static async login(data: { password: string; email: string }) {
        const response = await fetch("https://localhost:7161/login", {
            method: "POST",
            mode: "cors",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });
        if (response.status === 200) {
            return await response.json();
        }
        else {
            var result = "An error occured! ";
            var error = await response.json();
            error = JSON.stringify(error);
            return result + error;
        }
    }
}