import React from 'react';
import Button from './Button.js';
import "./App.css";
import './index.css';

export default function DeleteItem({id, setPCs}) {
  function getPCs() {
    return JSON.parse(localStorage.getItem("pcs")) || [];
  }

function savePCs(pcs) {
    localStorage.setItem("pcs", JSON.stringify(pcs));
  }

  function handleDelete() {
      const pcs = getPCs();
      const updatedPCs = pcs.filter(pc => pc.id !== id);
      savePCs(updatedPCs);
      setPCs(updatedPCs);
  }

  return (
   <Button text="Delete" onClick={handleDelete} className="delete-button" />
  );
}