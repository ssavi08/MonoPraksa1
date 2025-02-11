import { useState } from "react";
import "./App.css";
import './index.css';
import Button from "./Button";

export default function AddForm({ pcs, setPCs }) {
    const [pc, setPC] = useState({ name: "", cpu: "", gpu: "", refreshRate: "" });

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
        setPC({ id: "", name: "", cpu: "", gpu: "", refreshRate: "" });
    }

    const [value, setValue] = useState('');
    const option = [
        {label: "60Hz", value: 1},
        {label: "120Hz", value: 2},
        {label: "144Hz", value: 3},
    ];

    function handleSelect(event) {
        setValue(event.target.value);
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
                <div className="form-group">
                    <label>Refresh rate</label>
                    <select 
                        className="form-select"
                        name="refreshRate"
                        required
                        value={pc.refreshRate}
                        onChange={handleChange} 
                    >
                        {option.map((option) => (
                            <option value={option.value}>{option.label}</option>
                        ))}
                    </select>
                </div>
                <Button className="save-button" text="Save" type="submit" />
            </form>
        </div>
    );
}
