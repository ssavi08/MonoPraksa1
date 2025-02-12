import React from "react";
import axios from "axios";

class AppService {
    async getPCs() {
        const response = await axios.get("https://localhost:7190/api/PC/");
        return response.data;
    }

    async addPC(newPC) {
        const response = await axios.post("https://localhost:7190/api/PC/", newPC);
        return response.data;
    }
}

export default new AppService()

