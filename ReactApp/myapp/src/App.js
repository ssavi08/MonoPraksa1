
import "./App.css";
import AddForm from "./AddForm";
import Grid from "./Grid";
import { useEffect, useState } from "react";
import AppService from "./AppService";

export default function App() {
  const [pcs, setPCs] = useState([]);
  
  useEffect(() => {
    AppService.getPCs()
    .then(setPCs)
  }, []);

  return (
    <div>
      <header className="headerhead"><h1>PC Manager</h1></header>
        <h2>Add New PC</h2>
        <AddForm setPCs={setPCs} />
      { 
        pcs.length > 0 ? (
          <>
            <h2>Saved PCs:</h2>
            <Grid pcs={pcs} setPCs={setPCs}/>
          </>
        ) : (
          <div className="empty-list-message">No PCs saved yet</div>
        )
      }
    </div>
  );
}


