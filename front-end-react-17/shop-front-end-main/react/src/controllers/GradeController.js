import axios from "axios";

class GradeController {
    token = localStorage.getItem('token');

    async addGrade(grade) {
        if (this.token != null) {
            try {
                axios.defaults.headers.common['Authorization'] = 'Bearer ' + this.token;
                console.log("newgrade", grade);
                const response = await axios.post('/grade/save/', grade);

                alert("Inserted grade successfully!");
            } catch (error) {
                console.error("error-insert", error);
                alert("Insert failed", error);
            }
        }
        else {
            return null;
        }
    }

    async getGrades() {
        if (this.token != null) {
            try {
                axios.defaults.headers.common['Authorization'] = 'Bearer ' + this.token;
                const response = await axios.get('/grade/student');
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
export default new GradeController();