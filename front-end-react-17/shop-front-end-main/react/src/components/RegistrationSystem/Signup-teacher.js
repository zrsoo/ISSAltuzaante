import React from 'react';
import {Helmet} from "react-helmet";
import AuthenticationController from '../../controllers/AuthenticationController';
import { useState } from 'react';

export default function SignupTeacher() {
    const [email, setEmail] = useState();
    const [password, setPassword] = useState();
    const [firstName, setFirstName] = useState();
    const [lastName, setLastName] = useState();
    const [degree, setDegree] = useState();
    const [status, setStatus] = useState();
    const [faculty, setFaculty] = useState();

    const handleSubmit = e => {
        e.preventDefault();
        const data = {
            firstName: firstName,
            lastName: lastName,
            email: email,
            password: password,
            degree:degree,
            isChiefOfDepartment:status,
            facultyId: parseInt(faculty)
        };

        AuthenticationController.register_teacher(data);
    };

    return (
        <div className="auth-wrapper">
            <Helmet>
                <title>Register</title>
            </Helmet>
            <div className="auth-inner">
                <form onSubmit={handleSubmit}>
                    <h3>Sign Up</h3>
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

                    <div className="form-group">
                        <label>Email</label>
                        <input type="email" className="form-control" placeholder="Email"
                               onChange={e => setEmail(e.target.value)} />
                    </div>

                    <div className="form-group">
                        <label>Password</label>
                        <input type="password" className="form-control" placeholder="Password"
                               onChange={e => setPassword(e.target.value)} />
                    </div>

                    <div className="form-group">
                        <label>Degree</label>
                        <input type="text" className="form-control" placeholder="Degree"
                               onChange={e => setDegree(e.target.value)} />
                    </div>
                    <div className="form-group">
                        <label>IsChiefOfDepartment</label>
                        <input type="text" className="form-control" placeholder="true/false"
                               onChange={e => setStatus(e.target.value)} />
                    </div>
                    <div className="form-group">
                        <label>Faculty</label>
                        <input type="text" className="form-control" placeholder="Faculty"
                               onChange={e => setFaculty(e.target.value)} />
                    </div>

                    <button className="btn btn-primary btn-block submit-btn">Sign Up</button>

                </form>
            </div>
        </div>

    );
}