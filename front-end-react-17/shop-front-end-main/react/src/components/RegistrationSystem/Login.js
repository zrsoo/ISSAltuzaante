import React from 'react'
import HttpRequestController from '../../controller/HttpRequestController';
import AuthenticationController from '../../controllers/AuthenticationController';
import "../RegistrationSystem/Login.css";

export default class Login extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            user: 0
        };
    }

    handleSubmit = e => {
        e.preventDefault();

        const data = {
            email: this.email,
            password: this.password
        };

        AuthenticationController.login(data).then(res => {
            console.log("Login state user", res);
             this.setState({ user: res });
        });

    };

    render() {
        const { user } = this.state
        return (
            <div className="auth-wrapper">
                <div className="auth-inner login-wrap">
                    <form onSubmit={this.handleSubmit}>
                        <h3>Login</h3>
                        <div className="form-group">
                            <label>Email</label>
                            <input name ="email" type="email" className="form-control" placeholder="Email"
                                onChange={e => this.email = e.target.value} />
                        </div>

                        <div className="form-group">
                            <label>Password</label>
                            <input type="password" className="form-control" placeholder="Password"
                                onChange={e => this.password = e.target.value} />
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

}