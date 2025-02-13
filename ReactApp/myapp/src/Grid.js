import { useState } from "react";
import DeletePC from "./Delete";
import UpdateForm from "./UpdateForm";
import "./App.css";
import './index.css';
import { useNavigate } from "react-router-dom";
import Button from "./Button";

export default function Grid({pcs, setPCs}) {
    const [selectedPCId, setSelectedPCId] = useState(null);
    const navigate = useNavigate();

    return (
        <div>
            <h1>List of computers</h1>
            <div className="table-style">
                <table>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>CPU</th>
                            <th>GPU</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {pcs.map((pc) => (
                            <tr key={pc.id}>
                                <td>{pc.name}</td>
                                <td>{pc.cpuModelName}</td>
                                <td>{pc.gpuModelName}</td>
                                <td>
                                    <button className="update-button" onClick={() => setSelectedPCId(pc.id)}>Update</button>
                                    <DeletePC id={pc.id} setPCs={setPCs} />
                                </td>
                            </tr>
                        ))}
                    </tbody>
                    
                </table>
                {selectedPCId && (
                    <div className="update-form">
                        <UpdateForm id={selectedPCId} setPCs={setPCs} onClose={() => setSelectedPCId(null)}></UpdateForm>
                    </div>
                )}
            </div>
            <Button text="Go home" className="cancel-button" onClick={() => navigate("/")} />
        </div>
    );
}
