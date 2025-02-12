import React from "react";
import DeleteItem from "./DeleteItem";
import UpdateForm from "./UpdateForm";
import { useState, useEffect } from "react";
import "./App.css";
import './index.css';


export default function Grid() {
    const [pcs, setPCs] = useState(() => {
        return JSON.parse(localStorage.getItem("pcs")) || [];
    });

    if (pcs.length === 0) return (
        <p>No PCs saved yet</p>
    );

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
                        <td>{pc.cpu}</td>
                        <td>{pc.gpu}</td>
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
