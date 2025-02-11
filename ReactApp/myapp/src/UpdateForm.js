import React from "react";
import Button from "./Button.js";
import { useState } from "react";
import "./App.css";
import './index.css';

export default function UpdateForm({ id, setPCs }) {
  const [isEditing, setIsEditing] = useState(false);
  const [updatedPC, setUpdatedPC] = useState({ name: "", cpu: "", gpu: "" });

  function handleEditClick() {
      setIsEditing(true);
      const pcs = JSON.parse(localStorage.getItem("pcs")) || [];
      const pcToEdit = pcs.find(pc => pc.id === id);
      if (pcToEdit) {
          setUpdatedPC(pcToEdit);
      }
  }

  function handleChange(event) {
      const { name, value } = event.target;
      setUpdatedPC(prev => ({ ...prev, [name]: value }));
  }

  function handleUpdate() {
      const pcs = JSON.parse(localStorage.getItem("pcs")) || [];
      const updatedPCs = pcs.map(pc => (pc.id === id ? updatedPC : pc));
      localStorage.setItem("pcs", JSON.stringify(updatedPCs));
      setPCs(updatedPCs);
      setIsEditing(false);
  }

  return isEditing ? (
      <div className="container">
          <input type="text" name="name" value={updatedPC.name} onChange={handleChange} />
          <input type="text" name="cpu" value={updatedPC.cpu} onChange={handleChange} />
          <input type="text" name="gpu" value={updatedPC.gpu} onChange={handleChange} />
          <Button text="Save" onClick={handleUpdate} className="update-button" />
      </div>
  ) : (
      <Button text="Update" onClick={handleEditClick} className="update-button" />
  );
}
