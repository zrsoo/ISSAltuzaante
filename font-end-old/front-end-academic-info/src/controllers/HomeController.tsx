import {useEffect, useState} from "react";

export class HomeController {
    static async getHome(): Promise<string> {
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

export function useHome() {
    const [data, setData] = useState(null);

    useEffect(() => {
        (async () => {
            const response = await fetch("https://localhost:7161/api/Home/", {
                method: "get",
                mode: "cors",
                headers: {
                    "Content-Type": "application/json"
                },
            });
            const json = await response.json();
            setData(json);
        })()
    }, []);

    return data;
}
