import React from "react";
import Popup from "./Popup.js";
import "./App.css";
import './index.css';

export default function Button({text, onClick, className}) {
    
    return (
    <button className={className} onClick={onClick}>{text}</button>
    )
}