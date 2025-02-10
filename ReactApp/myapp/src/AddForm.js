import './App.css';
import Button from './Button';

export default function AddForm() {
    function handleSubmit(event) {
        event.preventDefault();
        
        const formData = new FormData(event.target);
        const name = formData.get("pcName");
        const cpu = formData.get("pcCpu");
        const gpu = formData.get("pcGpu");
        
        const newPC = {id: Date.now().toString(), name, cpu, gpu };

        const pcs = JSON.parse(localStorage.getItem("pcs")) || [];
        pcs.push(newPC);
        localStorage.setItem("pcs", JSON.stringify(pcs));
    }

  return (
    <form onSubmit={handleSubmit}>
        <div className="form-group">
            <label>PC Name</label>
            <input type="text" name="pcName" required />
        </div>
        <div className="form-group">
            <label>CPU</label>
            <input type="text" name="pcCpu" required />
        </div>
        <div className="form-group">
            <label>GPU</label>
            <input type="text" name="pcGpu" required />
        </div>
        <Button buttonText="Save PC" />
    </form>
  );
}
