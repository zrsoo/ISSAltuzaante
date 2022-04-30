import React from 'react';
import {Helmet} from "react-helmet";
import { useState } from 'react';
import UserController from "../../controllers/UserController";

export default function UpdateUser() {
    const [password, setPassword] = useState();


    const handleSubmit = e => {
        if(document.getElementsByName("1").values()[0] !== document.getElementsByName("2").values()[0]){
            alert("The password doesn't match!")
        }

        e.preventDefault();
        const data = {
            password: password,
        };
        UserController.update_password(data);
    };

    return (
        <div className="auth-wrapper">
            <Helmet>
                <title>Update Password</title>
            </Helmet>
            <div className="auth-inner">
                <form onSubmit={handleSubmit}>
                    <h3>Update Password</h3>

                    <div className="form-group">
                        <label>Password</label>
                        <input  name={"1"} type="password" className="form-control" placeholder="Password"
                               onChange={e => setPassword(e.target.value)} />
                    </div>

                    <div className="form-group">
                        <label>Repeat Password</label>
                        <input name={"2"} type="password" className="form-control" placeholder="Repeat Password"
                                />
                    </div>
                    <button className="btn btn-primary btn-block login-button">Update</button>

                </form>
            </div>
        </div>

    );
}