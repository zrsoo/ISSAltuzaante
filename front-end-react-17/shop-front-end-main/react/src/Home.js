import React from 'react'
import { useTranslation } from 'react-i18next'
import { useState, useEffect } from 'react';
import ReactLoading from "react-loading";
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

                {props.user.isStudent === "Yes" &&
                        <div>
                            <Link to="/view-curriculum" className="btn btn-primary login-button">View Curriculum</Link>
                        </div>
                    }
                      {props.user.isStudent === "No" &&
                        <div>
                             <Link to="/aprove-optionals" className="btn btn-primary login-button">Aprove-Optionals</Link>
                        </div>
                    }
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
