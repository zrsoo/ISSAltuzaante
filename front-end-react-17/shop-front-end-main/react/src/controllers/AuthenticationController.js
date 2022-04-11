import axios from "axios";

class AuthenticationController {
     async register(data) {
         try {
            const response = await axios.post('/register', data)     
         } catch (error) {
             console.log(error);
             alert("ERROR Try again!");
             return;
         }
         alert("Succes. Log in!");
        
        //console.log("RESPONSE", response);
        // if (response.status === 200) {
        //     console.log(response);
        //     return await response.json();
        // }
        // else {
        //     var result = "An error occured! ";
        //     var error = await response.json();
        //     error = JSON.stringify(error);
        //     return result + error;
        // }
    }

    async getUser(){
        if (localStorage.getItem('token') != null) {
            try {
                axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('token');
                const response = await axios.get('/get-authenticated-user');
                //console.log(response);
                return response.data;
            } catch (error) {
                console.error(error);
            }
        }
        else {
            return null;
        }
        // return await fetch("https://localhost:7161/get-authenticated-user", {
        //     method: "GET",
        //     mode: "cors",
        //     headers: {
        //         "Content-Type": "application/json",
        //         "Authorization": "Bearer " + token
        //     }
        // })
    }

    async login(data) {
        // const response = await fetch("https://localhost:7161/login", {
        //     method: "POST",
        //     mode: "cors",
        //     headers: {
        //         "Content-Type": "application/json",
        //     },
        //     body: JSON.stringify(data),
        // });
        let user = "";
        try {
            const response = await axios.post('/login', data);
            localStorage.setItem('token', response.data.token);
            console.log("RESPONSE", response.data);
            user = response.data;
        } catch(error) {
            console.log(error);
            user = "bad-credentials";
        }
        
        return user;
    }

}
export default new AuthenticationController();