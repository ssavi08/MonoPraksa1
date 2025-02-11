import React from "react";
import Popup from "./Popup.js";

export default function Button({text, onClick }) {
    
    return (
    <button onClick={onClick}>{text}</button>
    )
}