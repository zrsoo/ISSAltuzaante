import axios from "axios";

class AuthenticationController {
     async register_student(data) {
         try {
            const response = await axios.post('/Authenticate/register-student', data)
         } catch (error) {
             console.log("ERROR", error);
             //alert("ERROR Try again!");
             alert(error.response.data.errors);
             return;
         }
         alert("Succes. Log in!");
    }

    async register_teacher(data) {
        try {
            const response = await axios.post('/Authenticate/register-teacher', data)
        } catch (error) {
            console.log(error);
            //alert("ERROR Try again!");
            alert(error.response.data.errors);
            return;
        }
        alert("Succes. Log in!");
    }

    async getUser(){
        if (localStorage.getItem('token') != null) {
            try {
                axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('token');
                const response = await axios.get('/Authenticate/get-authenticated-user');
                //console.log(response);
                return response.data;
            } catch (error) {
                console.error(error);
            }
        }
        else {
            return null;
        }
    }

    async login(data) {
        let user = "";
        try {
            const response = await axios.post('/Authenticate/login', data);
            console.log("pl")
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