import logo from './logo.svg';
import {useState} from 'react';
import './App.css';

function MyButton({onClick}) {
  const [count, setCount] = useState(0);

  function handleClick1() {
    setCount(count + 1);
    onClick();
  }

  return (
    <div>
      <button onClick={handleClick1}>Display
      </button>
      <p>Pogledao si listu {count} puta</p>
    </div>
  );
}

const products = [
  { name: 'Jabuka', isFruite: true , id: 1},
  { name: 'Kruska', isFruite: true , id: 2},
  {name: 'Krumpir', isFruite: false, id: 3}
]

function ShoppingList() {
  const listItems = products.map(product =>
    <li
      key={product.id}
      style ={{
        color: product.isFruite ? 'green' : 'red'
      }}
    >
      {product.name}
    </li>
  )

  return (
    <ul>{listItems}</ul>
  );
}

function App() {
  const [showShoppingList, setShowShoppingList] = useState(false);

  function handleClick(){
    setShowShoppingList(true);
  }

  return (
    <div>
      <h1>Lista za kupovinu</h1>
      <MyButton onClick={handleClick} />
      {showShoppingList && <ShoppingList />}
    </div>
  );
}
export default App;
