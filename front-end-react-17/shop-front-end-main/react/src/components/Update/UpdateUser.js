import React from 'react';
import {Helmet} from "react-helmet";
import { useState } from 'react';
import UserController from "../../controllers/UserController";

export default function UpdateUser() {
    //const [email, setEmail] = useState();
    //const [password, setPassword] = useState();
    const [firstName, setFirstName] = useState();
    const [lastName, setLastName] = useState();

    const handleSubmit = e => {
        e.preventDefault();
        const data = {
            firstName: firstName,
            lastName: lastName,
            //email: email,
            //password: password,
        };
        UserController.update_user(data);
    };

    return (
        <div className="auth-wrapper">
            <Helmet>
                <title>Update User</title>
            </Helmet>
            <div className="auth-inner">
                <form onSubmit={handleSubmit}>
                    <h3>Update Profile</h3>
                    <div className="form-group">
                        <label>First Name</label>
                        <input type="text" className="form-control" placeholder="First Name"
                               onChange={e => setFirstName(e.target.value)} />
                    </div>

                    <div className="form-group">
                        <label>Last Name</label>
                        <input type="text" className="form-control" placeholder="Last Name"
                               onChange={e => setLastName(e.target.value)} />
                    </div>

                    {/*<div className="form-group">*/}
                    {/*    <label>Password</label>*/}
                    {/*    <input type="password" className="form-control" placeholder="Password"*/}
                    {/*           onChange={e => setPassword(e.target.value)} />*/}
                    {/*</div>*/}
                    <button className="btn btn-primary btn-block login-button">Update</button>

                </form>
            </div>
        </div>

    );
}