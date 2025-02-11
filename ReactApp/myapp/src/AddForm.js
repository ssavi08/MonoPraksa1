import { useState } from "react";
import "./App.css";
import Button from "./Button";

export default function AddForm({ pcs, setPCs }) {
    const [pc, setPC] = useState({ name: "", cpu: "", gpu: "" });

    function handleChange(event) {
        const { name, value } = event.target;
        setPC((prevPC) => ({ ...prevPC, [name]: value }));
    }

    function handleSubmit(event) {
        const newPC = { id: Date.now().toString(), ...pc };
        const updatedPCs = [...pcs, newPC];
        setPCs(updatedPCs); // Updates the state in App.js
    }

    return (
        <div>
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>PC Name</label>
                    <input 
                        type="text" 
                        name="name" 
                        required 
                        value={pc.name} 
                        onChange={handleChange} 
                    />
                </div>
                <div className="form-group">
                    <label>CPU</label>
                    <input 
                        type="text" 
                        name="cpu" 
                        required 
                        value={pc.cpu} 
                        onChange={handleChange} 
                    />
                </div>
                <div className="form-group">
                    <label>GPU</label>
                    <input 
                        type="text" 
                        name="gpu" 
                        required 
                        value={pc.gpu} 
                        onChange={handleChange} 
                    />
                </div>
                <Button text="Save PC" type="submit" />
            </form>
        </div>
    );
}
