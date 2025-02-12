import React from "react";
import DeleteItem from "./DeleteItem";
import UpdateForm from "./UpdateForm";
import "./App.css";
import './index.css';

export default function Grid({pcs, setPCs}) {

    return (
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
                            <UpdateForm id={pc.id} setPCs={setPCs} />
                            <DeleteItem id={pc.id} setPCs={setPCs} />
                        </td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}
