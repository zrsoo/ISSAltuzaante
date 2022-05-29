import React, { useState } from 'react'
import "../src/components/Loading/loading.css"
import Login from './components/RegistrationSystem/Login';
import { Link } from 'react-router-dom';
import {Helmet} from 'react-helmet';
import UserController from './controllers/UserController';

export default function Home(props) {   
    const [contract, setContract] = useState(0);

    function signContract() {
        UserController.SignContract();
    }
    if (props.user) {
        return (
            <div>
                <Helmet>
                    <title>Home</title>
                </Helmet>
                <h1>Hello, {props.user.firstName}. Welcome to academic info!</h1>
                <div className='home-buttons'>
                {props.user.isStudent === "Yes" &&  
                        <div>
                            <Link to="/view-curriculum" className="btn btn-primary login-button">View Curriculum</Link>
                            <Link to="/grades" className="btn btn-primary login-button">View Grades</Link>
                            <Link to="/view-optionals" className="btn btn-primary login-button">View Optionals</Link>
                            {
                                props.user.isApproved === true && props.user.hasSigned == false &&
                                <div className="sign-div">
                                    <input placeholder='Sign with your name' onChange={e => setContract(e.target.value)}></input>
                                    <button className="btn btn-success" onClick={() => signContract()}>Sign contract</button>
                                </div>
                            }
                            {
                                props.user.isApproved === false &&
                                <div className="sign-div">
                                    <h3>Wait until the chief of department approves the optionals!</h3>
                                    <input placeholder='Enter your name'></input>
                                    <button className="btn btn-success" disabled>Sign contract</button>
                                </div>
                            }
                            {
                                props.user.isApproved === true && props.user.hasSigned == true &&
                                <div className="sign-div">
                                    <h2>You are registered for 2022-2023 academic year!</h2>
                                </div>
                            }
                        </div>
                    }
                {props.user.isStudent === "No" &&
                        <div>
                             <div><Link to="/aprove-optionals" className="btn btn-primary login-button">Approve-Optionals</Link></div>
                             <div><Link to="/add-optionals" className='btn btn-primary login-button'>Add Optionals</Link></div>
                             <div><Link to="/view-disciplines-teacher" className='btn btn-primary login-button'>View Teaching Discplines</Link></div>
                             <div><Link to="/view-discipline-rankings" className='btn btn-primary login-button'>View Discipline Rankings</Link></div>
                             <div><Link to="/view-disciplines-per-teacher-year" className='btn btn-primary login-button'>View Disciplines By Teacher And Year</Link></div>
                        </div>
                    }
                </div>
            </div>
        )   
    }
    else {
        return (
            <div>
                <Helmet>
                    <title>Home</title>
                </Helmet>
                <div className="home-login-div">
                    <Login className="home-login"></Login>
                </div>
                <div className='home-sign-up-div'>
                    <h3>Not_registered?</h3> 
                    <Link to="/signup" className="btn btn-primary login-button">Sign-Up</Link>
                </div>
            </div>
        )   
    }
}
