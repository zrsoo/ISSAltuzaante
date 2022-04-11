import React from 'react';
import {Helmet} from "react-helmet";
import AuthenticationController from '../../controllers/AuthenticationController';

class Signup extends React.Component {

    handleSubmit = e => {
        e.preventDefault();
        const data = {
            firstName: this.firstName,
            lastName: this.lastName,
            email: this.email,
            password: this.password,
            city: this.city, 
            year: parseInt(this.year)
        };

        AuthenticationController.register(data);
    };

    render() {
        return (
            <div className="auth-wrapper">
                <Helmet>
                    <title>Register</title>
                </Helmet>
                <div className="auth-inner">
                    <form onSubmit={this.handleSubmit}>
                        <h3>Sign Up</h3>
                        <div className="form-group">
                            <label>First Name</label>
                            <input type="text" className="form-control" placeholder="First Name"
                                onChange={e => this.firstName = e.target.value} />
                        </div>

                        <div className="form-group">
                            <label>Last Name</label>
                            <input type="text" className="form-control" placeholder="Last Name"
                                onChange={e => this.lastName = e.target.value} />
                        </div>

                        <div className="form-group">
                            <label>Email</label>
                            <input type="email" className="form-control" placeholder="Email"
                                onChange={e => this.email = e.target.value} />
                        </div>

                        <div className="form-group">
                            <label>Password</label>
                            <input type="password" className="form-control" placeholder="Password"
                                onChange={e => this.password = e.target.value} />
                        </div>

                        <div className="form-group">
                            <label>City</label>
                            <input type="text" className="form-control" placeholder="City"
                                onChange={e => this.city = e.target.value} />
                        </div>
                        <div className="form-group">
                            <label>Year</label>
                            <input type="text" className="form-control" placeholder="Year"
                                onChange={e => this.year = e.target.value} />
                        </div>

                        <button className="btn btn-primary btn-block submit-btn">Sign Up</button>

                    </form>
                </div>
            </div>

        );
    }
}

export default Signup;