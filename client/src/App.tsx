
import './App.css'
import robots from './mockdata.json';
import Robot from './components/Robot';

function App() {

  return (
    <ul>
        {robots.map(r => (
            <Robot key={r.id} id={r.id} name={r.name} email={r.email} />
        ))}
    </ul>
  )
}

export default App
