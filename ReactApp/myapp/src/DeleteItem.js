import React from 'react';
import Button from './Button.js';

function getPCs() {
    return JSON.parse(localStorage.getItem("pcs")) || [];
  }

function savePCs(pcs) {
    localStorage.setItem("pcs", JSON.stringify(pcs));
  }

export default function DeleteItem({id}) {
    function handleDelete() {
        const pcs = getPCs();

        const updatedPCs = pcs.filter(pc => pc.id !== id);

        savePCs(updatedPCs);
    }

    return (
        <Button buttonText="Delete" onClick={handleDelete}> </Button>
    );
}