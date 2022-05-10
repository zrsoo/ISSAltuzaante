import * as React from 'react';
import { Suspense } from 'react';
import Navbar from "./components/Navbar/Navbar";
import './App.css';
import Login from './components/RegistrationSystem/Login';
import SignupStudent from './components/RegistrationSystem/Signup-student';
import SignupTeacher from './components/RegistrationSystem/Signup-teacher';
import Home from './Home';
import { Route } from 'react-router-dom';
import '../node_modules/bootstrap/dist/css/bootstrap.min.css';
import Logout from './components/Logout/Logout';
import AuthenticationController from './controllers/AuthenticationController';
import UserController from "./controllers/UserController";
import UpdateUser from "./components/Update/UpdateUser";
import UpdatePassword from "./components/Update/UpdatePassword";
import AddOptionals from './components/Optionals/addOptionals';
import AproveOptionals from './components/AproveOptionals/AproveOptionals';
import ViewCurriculum from './components/ViewCurriculum/ViewCurriculum';


export default function App() {
  const [user, setUser] = React.useState();
  
  React.useEffect(() => {
    AuthenticationController.getUser().then((response) => {
      setUser(response);
      //console.log(response)
    });
    UserController.getUser().then((response) => {
      setUser(response);
      console.log(response)});
  } ,[]);


  
  return (
      <div className="App">
        <Suspense fallback={"Loading..."}>
          <Navbar user={user} />
          <Route exact path="/home" component={() => <Home user={user} />} />
          <Route exact path="/login" component={Login} />
          <Route exact path="/signup-student" component={SignupStudent} />
          <Route exact path="/signup-teacher" component={SignupTeacher} />
          <Route exact path="/logout" component={Logout} />
          <Route exact path="/updateuser" component={UpdateUser} />
          <Route exact path="/update-password" component={UpdatePassword} />
          <Route exact path="/addOptionals" component={AddOptionals}/>
          <Route exact path="/aprove-optionals" component={AproveOptionals} />
          <Route exact path="/view-curriculum" component={ViewCurriculum} />
        </Suspense>
      </div>
  )
}