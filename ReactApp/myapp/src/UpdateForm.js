import React from "react";
import Button from "./Button.js";
import { useState, useEffect } from "react";
import "./App.css";
import './index.css';
import AppService from "./AppService.js";

export default function UpdateForm({ id, setPCs, onClose }) {
  const [updatedPC, setUpdatedPC] = useState({ name: "", cpuModelName: "", gpuModelName: "" });

  useEffect(() => {
    async function fetchPC() {
        try{
            const data = await AppService.getPCById(id);
            setUpdatedPC(data);
        } catch (error) {
            console.error("Failed to fetch data", error);
        }
    }
    fetchPC();
  }, [id]);

  function handleChange(event) {
      const { name, value } = event.target;
      setUpdatedPC(prev => ({ ...prev, [name]: value }));
  }

  function handleUpdate() {
      AppService.updatePC(id, updatedPC)
        .then(() => AppService.getPCs().then(setPCs)) //refreshing pc list
        .then(onClose)
        .catch((error) => console.error("Update failed.", error));
  }

  return (
            <div className="update-form">
                <h2>Update PC</h2>
                <input type="text" placeholder={updatedPC.name} name="name" value={updatedPC.name} onChange={handleChange} />
                <input type="text" placeholder={updatedPC.cpuModelName} name="cpuModelName" value={updatedPC.cpuModelName} onChange={handleChange} />
                <input type="text" name="gpuModelName" value={updatedPC.gpuModelName} onChange={handleChange} />
                <br/>
                <div className="option-button">
                    <Button text="Save" onClick={handleUpdate} className="save-button" />
                    <Button text="Cancel" onClick={onClose} className="update-button" />
                </div>
            </div>
    )
}
