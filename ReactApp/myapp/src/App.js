
import "./App.css";
import AddForm from "./AddForm";
import Grid from "./Grid";
import { useState, useEffect } from "react";

function App() {
  function getPCs() {
      return JSON.parse(localStorage.getItem("pcs")) || [];
  }

  const [pcs, setPCs] = useState(() => getPCs());

  useEffect(() => {
      localStorage.setItem("pcs", JSON.stringify(pcs));
  }, [pcs]);

  return (
    <div>
      <h1>PC Manager</h1>
      <h2>Add New PC</h2>
      <AddForm pcs={pcs} setPCs={setPCs} />

      {pcs.length > 0 && (
          <>
              <h2>Saved PCs:</h2>
              <Grid />
          </>
      )}
    </div>
  );
}

export default App;
