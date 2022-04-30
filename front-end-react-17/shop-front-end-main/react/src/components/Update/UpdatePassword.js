import React from 'react';
import {Helmet} from "react-helmet";
import { useState } from 'react';
import UserController from "../../controllers/UserController";

export default function UpdateUser() {
    const [oldPassword, setOldPassword] = useState();
    const [newPassword, setNewPassword] = useState();


    const handleSubmit = e => {
        if(document.getElementsByName("newPassInput").values()[0] !== document.getElementsByName("oldPassInput").values()[0]){
            alert("The password doesn't match!")
        }
        else {
            e.preventDefault();
            const data = {
                password: oldPassword,
                newPassword: newPassword,
            };
            UserController.update_password(data);
        }
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
                        <label>Old Password</label>
                        <input type="password" className="form-control" placeholder="Password"
                                onChange={e => setOldPassword(e.target.value)} />
                    </div>

                    <div className="form-group">
                        <label>New Password</label>
                        <input name={"newPassInput"} type="password" className="form-control" placeholder="Password"
                               onChange={e => setNewPassword(e.target.value)} />
                    </div>

                    <div className="form-group">
                        <label>Repeat New Password</label>
                        <input name={"oldPassInput"} type="password" className="form-control" placeholder="Repeat Password"
                                />
                    </div>
                    <button className="btn btn-primary btn-block login-button">Update</button>

                </form>
            </div>
        </div>

    );
}