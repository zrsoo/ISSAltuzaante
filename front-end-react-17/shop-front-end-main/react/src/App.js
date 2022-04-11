import * as React from 'react';
import { Suspense } from 'react';
import Navbar from "./components/Navbar/Navbar";
import './App.css';
import Login from './components/RegistrationSystem/Login';
import Signup from './components/RegistrationSystem/Signup';
import Home from './Home';
import { Route } from 'react-router-dom';
import '../node_modules/bootstrap/dist/css/bootstrap.min.css';
import Logout from './components/Logout/Logout';
import AuthenticationController from './controllers/AuthenticationController';

export default class App extends React.Component {
  state = {
  };

  async setUser() {
    this.setState({
      user: await AuthenticationController.getUser()
    });
  }

  componentDidMount = () => {
    this.setUser();
  };

  render() {
    const { user } = this.state;

    return (
      <div className="App">
        <Suspense fallback={"Loading..."}>
          <Navbar user={user} />
          <Route exact path="/home" component={() => <Home user={user} />} />
          <Route exact path="/login" component={Login} />
          <Route exact path="/signup" component={Signup} />
          <Route exact path="/logout" component={Logout} />
        </Suspense>
      </div>
    );
  }
}
