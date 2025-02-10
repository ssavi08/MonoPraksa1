import React from "react";
import Popup from "./Popup.js";

export default function Button({buttonText, onClick }) {
    
    return (
    <button onClick={onClick}>{buttonText}</button>
    )
}