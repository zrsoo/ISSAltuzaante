import React from 'react'
import "../src/components/Loading/loading.css"
import Login from './components/RegistrationSystem/Login';
import { Link } from 'react-router-dom';
import {Helmet} from 'react-helmet';

export default function Home(props) {    
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
                        </div>
                    }
                {props.user.isStudent === "No" &&
                        <div>
                             <div><Link to="/aprove-optionals" className="btn btn-primary login-button">Approve-Optionals</Link></div>
                             <div><Link to="/add-optionals" className='btn btn-primary login-button'>Add Optionals</Link></div>
                             <div><Link to="/view-disciplines-teacher" className='btn btn-primary login-button'>View Teaching Discplines</Link></div>
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
