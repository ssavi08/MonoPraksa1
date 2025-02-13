import { useState, useEffect } from "react";
import AppService from "./AppService";

export default function AddForm({ setPCs }) {
    const initialPC = { name: "", cpuModelName: "", gpuModelName: "" };
    const [newPC, setNewPC] = useState(initialPC);

    function handleChange(event) {
        const { name, value } = event.target;
        setNewPC((prevPC) => ({ ...prevPC, [name]: value }));
    }

    async function handleSubmit(event) {
        event.preventDefault();
        try {
            await AppService.addPC(newPC);
            const updatedPCs = await AppService.getPCs();
            setPCs(updatedPCs);
            setNewPC(initialPC);
            
        } catch (error) {
            console.error("Error adding PC: ", error);
        }
    }

    return (
        <form onSubmit={handleSubmit}>
            <div className="form-group">
                <label>PC Name</label>
                <input
                    type="text"
                    name="name"
                    value={newPC.name}
                    onChange={handleChange}
                    required
                />
                <label>CPU</label>
                <input
                    type="text"
                    name="cpuModelName"
                    value={newPC.cpuModelName}
                    onChange={handleChange}
                    required
                />
                <label>GPU</label>
                <input
                    type="text"
                    name="gpuModelName"
                    value={newPC.gpuModelName}
                    onChange={handleChange}
                    required
                />
            </div>
            <button type="submit" className="save-button">Add PC</button>
        </form>
    );
}
