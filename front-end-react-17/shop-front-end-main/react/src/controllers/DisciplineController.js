import axios from "axios";

class DisciplineController {
    token = localStorage.getItem('token');
    async getOptionalDisciplines(){
        if (this.token != null) {
            try {
                axios.defaults.headers.common['Authorization'] = 'Bearer ' + this.token;
                const response = await axios.get('/discipline/get-all-optionals');
                return response.data;
            } catch (error) {
                console.error(error);
            }
        }
        else {
            return null;
        }
    }

    async updateDiscipline(newDiscipline) {
        if (this.token != null) {
            try {
                axios.defaults.headers.common['Authorization'] = 'Bearer ' + this.token;
                console.log("new", newDiscipline);
                const response = await axios.patch('/discipline/update/' + newDiscipline.disciplineId, newDiscipline);

                alert("Updated successfully!");
            } catch (error) {
                console.error("eroare-update", error);
                alert("Update failed", error);
            }
        }
        else {
            return null;
        }
    }
}
export default new DisciplineController();