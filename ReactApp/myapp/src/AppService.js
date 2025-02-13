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

    async getPCById(id){
        const response = await axios.get(`https://localhost:7190/api/PC/${id}`);
        return response.data;
    }

    async deletePC(id) {
        await axios.delete(`https://localhost:7190/api/PC/${id}`);
    }

    async updatePC(id, updatedPC) {
        await axios.put(`https://localhost:7190/api/PC/${id}`, updatedPC);
    }
    
}

export default new AppService()

