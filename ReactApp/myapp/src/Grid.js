import React from "react";
import DeleteItem from "./DeleteItem";
import UpdateForm from "./UpdateForm";

function getPCs() {
    return JSON.parse(localStorage.getItem("pcs")) || [];
  }

export default function Grid(){
    const pcs = getPCs();
  return (
    <table striped bordered hover size="sm">
      <thead>
        <tr>
          <th>Name</th>
          <th>CPU</th>
          <th>GPU</th>
        </tr>
      </thead>
      <tbody>
        {pcs.map((pc) => (
          <tr key={pc.id}>
            <td>{pc.name}</td>
            <td>{pc.cpu}</td>
            <td>{pc.gpu}</td>
            <td>
              <DeleteItem id = {pc.id} />
              {/* <UpdateForm pc={pc} /> */}
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
