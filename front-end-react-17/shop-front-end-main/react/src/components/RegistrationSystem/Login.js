import React from 'react'
import AuthenticationController from '../../controllers/AuthenticationController';
import "../RegistrationSystem/Login.css";
import { useState } from 'react';

export default function Login(props) {
    const [user, setUser] = useState(0);
    const [email, setEmail] = useState();
    const [password, setPassword] = useState();

    const handleSubmit = e => {
        e.preventDefault();

        const data = {
            email: email,
            password: password
        };

        AuthenticationController.login(data).then(res => {
            console.log("Login state user", res);
             setUser(res);
        });
    };

    return (
        <div className="auth-wrapper">
            <div className="auth-inner login-wrap">
                <form onSubmit={handleSubmit}>
                    <h3>Login</h3>
                    <div className="form-group">
                        <label>Email</label>
                        <input name ="email" type="email" className="form-control" placeholder="Email"
                            onChange={e => setEmail(e.target.value)} />
                    </div>

                    <div className="form-group">
                        <label>Password</label>
                        <input type="password" className="form-control" placeholder="Password"
                            onChange={e => setPassword(e.target.value)} />
                    </div>
                    <button className="btn btn-primary btn-block login-button">Login</button>
                    {user === "bad-credentials" &&
                        <div className="bad-login">
                            <i className="fa fa-times-circle"></i>
                            Invalid email or password. Try again.
                        </div>
                    }
                    {user !== 0 && user !== "bad-credentials" &&
                        <div className="successful-login">
                            <i className="fa fa-check"></i>
                            Successful login!
                            <a href={"/home"}> Start browsing</a>
                        </div>
                    }
                </form>
            </div>
        </div>

    );
}