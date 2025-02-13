import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import AppService from "./AppService";
import Button from "./Button";

export default function AddForm({ setPCs }) {
  const initialPC = { name: "", cpuModelName: "", gpuModelName: "" };
  const [newPC, setNewPC] = useState(initialPC);
  const navigate = useNavigate();

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
      navigate("/pclist"); // Redirect to PC List after adding
    } catch (error) {
      console.error("Error adding PC: ", error);
    }
  }

  return (
    <div>
      <h2>Add New PC</h2>
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
            <Button text="Cancel" className="cancel-button" onClick={() => navigate("/")} />
        </form>
    </div>
  );
}
