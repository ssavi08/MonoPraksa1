import React from 'react';
import { useNavigate } from 'react-router-dom';
import Button from './Button';

export default function Home() {
  const navigate = useNavigate();

  return (
    <div>
        <h2>PC Management</h2>
        <nav>
          <ul>
            <li>
              <Button text="Add Computers" onClick={() => navigate("/addpc")} className="home-button"/>
            </li>
            <li>
              <Button text="List of Computers" onClick={() => navigate("/pclist")} className="home-button"/>
            </li>
          </ul>
        </nav>
    </div>
  );
};


