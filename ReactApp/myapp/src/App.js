import './App.css';
import AddForm from './AddForm';
import Grid from './Grid.js';

function App() {
  return(
    <div>
      <h1>PC Manager</h1>

      <h2>Add New PC</h2>
      <AddForm />

      <h2>Saved PCs:</h2>
      <Grid />
  </div>
  );
}

export default App;