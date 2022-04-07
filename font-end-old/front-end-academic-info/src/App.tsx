import React from 'react';
import logo from './logo.svg';
import './App.css';
import {HomeController, useHome} from './controllers/HomeController';

function App() {
  const home = useHome();
  const [testResponse, setTestResponse] = React.useState("");
  React.useEffect(() => {
    HomeController.getHome().then((response) => {
      console.log(response);
      setTestResponse(response);
    });
  }, [])

  if (!home) {

    return null;
  }
  
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Hello from Cluj! {testResponse}
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
