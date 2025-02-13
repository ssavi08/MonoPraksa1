import { useState } from 'react';
import Button from './Button.js';
import "./App.css";
import './index.css';
import AppService from './AppService.js';


export default function DeletePC({id, setPCs}) {
  const [showConfirm, setShowConfirm] = useState(false);

  function handleDelete(){
    setShowConfirm(true);
  }
  function deletePC(confirm){
    if (confirm) {
        AppService.deletePC(id)
        .then(() => AppService.getPCs()
          .then(setPCs));
    }
    setShowConfirm(false);
  }

  return (
    <div className="ConfirmDelete">
      <Button text="Delete" onClick={handleDelete} className="button" />
      {showConfirm && (
        <div>
          <Button text="Yes" onClick={() => deletePC(true)} className="delete-button" />
          <Button text="No" onClick={() => deletePC(false)} className="save-button" />
      </div>
      )}
    </div>
  );
}