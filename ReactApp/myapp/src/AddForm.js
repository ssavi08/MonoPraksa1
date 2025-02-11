import { useState } from "react";
import "./App.css";
import './index.css';
import Button from "./Button";

export default function AddForm({ pcs, setPCs }) {
    const [pc, setPC] = useState({ name: "", cpu: "", gpu: "" });

    function handleChange(event) {
        const { name, value } = event.target;
        setPC((prevPC) => ({ ...prevPC, [name]: value }));
    }

    function savePCs(pcs) {
        localStorage.setItem("pcs", JSON.stringify(pcs));
    }

    function handleSubmit(event) {
        console.log(pc);
        console.log(pcs);
        const newPC = { id: Date.now().toString(), ...pc };
        const updatedPCs = [...pcs, newPC];
        savePCs(updatedPCs); 
        setPCs(updatedPCs); // Updates the state in App.js
        setPC({ id: "", name: "", cpu: "", gpu: "" });
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
                <Button className="save-button" text="Save" type="submit" />
            </form>
        </div>
    );
}
