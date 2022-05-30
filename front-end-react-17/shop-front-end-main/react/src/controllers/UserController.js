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
    async getTeachersEmails() {
        if (localStorage.getItem('token') != null) {
            try {
                axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('token');
                const response = await axios.get('/User/get-teacher-emails');
                console.log(response);

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

    async approveOptionals() {
        console.log("approve din user")
        if (localStorage.getItem('token') != null) {
            try {
                axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('token');
                const response = await axios.get('/User/approve-optionals');
                console.log("Raspuns", response);
                alert("Optionals have been approved.");
            } catch (error) {
                console.error("error approve", error);
                alert("Approval failed", error);

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

    async SignContract() {
        if (localStorage.getItem('token') != null) {
            try {
                axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('token');
                const response = await axios.get('/User/sign-contract');
                alert("Sign contract.");
            } catch (error) {
                console.error("sign contract", error);
                alert("Sign contract", error);

            }
        }
        else {
            return null;
        }
    }
}
export default new UserController();