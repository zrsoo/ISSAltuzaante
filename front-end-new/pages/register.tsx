import { useEffect, useState } from "react";
import {AuthenticationController} from "../controllers/AuthenticationController";

export default function RegisterPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [city, setCity] = useState("");
  const [year, setYear] = useState("");
  // const register = async () => {

  // };

    const handleSubmit = e => {
        e.preventDefault();
        const data = {
            firstName: firstName,
            lastName: lastName,
            email: email,
            password: password,
            city: city,
            year: parseInt(year)
            //password_confirm: this.confirmPassword
        };
        console.log(data);
        //HttpRequestController.registrationPost(data)
        AuthenticationController.register(data).then((response) => console.log(response));
    }

  return (
    <div>
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
                    <label>City</label>
                    <input type="text" className="form-control" placeholder="City"
                           onChange={e => setCity(e.target.value)} />
                </div>
                <div className="form-group">
                    <label>Year</label>
                    <input type="text" className="form-control" placeholder="Year"
                           onChange={e => setYear(e.target.value)} />
                </div>
                <button className="btn btn-primary btn-block submit-btn">Sign Up</button>
            </form>
        </div>
    </div>
  );
}
