import React, { useState, useEffect } from "react";
import { Routes, Route } from "react-router-dom"; 
import Home from "./Home";
import AddForm from "./AddForm";
import Grid from "./Grid";
import AppService from "./AppService";

export default function App() {
  const [pcs, setPCs] = useState([]);

  useEffect(() => {
    AppService.getPCs().then(setPCs);
  }, []);

  return (
    <div>
      {/* Routes only */}
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/addpc" element={<AddForm setPCs={setPCs} />} />
        <Route path="/pclist" element={<Grid pcs={pcs} setPCs={setPCs} />} />
      </Routes>
    </div>
  );
}
