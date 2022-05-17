import axios from "axios";

class UserController {
    async update_user(data) {
        try {
            //console.log("pt")
            const response = await axios.patch('/User/updateuser', data)
        } catch (error) {
            console.log(error);
            //alert("ERROR Try again!");
            alert(error.response.data.errors);
            return;
        }
        alert("Succes. User updated!");
    }

    async update_password(data) {
        try {
            //console.log("pt")
            const response = await axios.patch('/User/update-password', data)
        } catch (error) {
            console.log(error);
            //alert("ERROR Try again!");
            alert(error.response.data.message);
            return;
        }
        alert("Succes. Password updated!");
    }

    async getUser(){
        if (localStorage.getItem('token') != null) {
            try {
                axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('token');
                const response = await axios.get('/User/get-authenticated-user');
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

    async getStatistics(id){
        if (localStorage.getItem('token') != null) {
            try {
                axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('token');
                const response = await axios.get('/User/group-statistics/' + id);
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

    async getStatisticsYear(id){
        if (localStorage.getItem('token') != null) {
            try {
                axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('token');
                const response = await axios.get('/User/year-statistics/' + id);
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

    async grantScholarships(id){
        if (localStorage.getItem('token') != null) {
            try {
                axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('token');
                const response = await axios.get('/User/grant-scholarships/' + id);
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
}
export default new UserController();