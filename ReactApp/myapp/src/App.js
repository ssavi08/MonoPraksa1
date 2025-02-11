
import "./App.css";
import AddForm from "./AddForm";
import Grid from "./Grid";
import { useState } from "react";

function App() {
  function getPCs() {
      const storagePCs = JSON.parse(localStorage.getItem("pcs")) || [];
      console.log(storagePCs);
      return storagePCs;
  }

  const [pcs, setPCs] = useState(() => getPCs());

  return (
    <div>
      <header className="headerhead"><h1>PC Manager</h1></header>
        <h2>Add New PC</h2>
        <AddForm pcs={pcs} setPCs={setPCs} />

      {
        pcs.length > 0 ? (
          <>
            <h2>Saved PCs:</h2>
            <Grid />
          </>
        ) : (
          <div className="empty-list-message">No PCs saved yet</div>
        )
      }
    </div>
  );
}

export default App;
