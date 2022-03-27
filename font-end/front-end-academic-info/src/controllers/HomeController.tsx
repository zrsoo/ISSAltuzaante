export class HomeController {
    static async getHome() : Promise<string>{
            return await fetch("https://localhost:7161/api/Home/", {
                method: "get",
                mode: "cors",
                headers: {
                 "Content-Type": "application/json"
            },
            }).then((response) => {
            if (response.ok) {
                return response.json();
            } else {
                throw new Error("Tzapa");
            }
        });
    }
}

