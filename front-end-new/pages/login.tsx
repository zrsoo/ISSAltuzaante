import {useState} from "react";
import {AuthenticationController} from "../controllers/AuthenticationController";

export default function LoginPage() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const handleSubmit = e => {
        e.preventDefault();
        const data = {
            email: email,
            password: password,
        };
        console.log(data);
        AuthenticationController.login(data).then((response) => {
            localStorage.setItem('token', response.token);
            console.log(response)
        });
    }
  return (
          <div>
              <div className="auth-inner">
                  <form onSubmit={handleSubmit}>
                      <h3>Log in</h3>
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

                      <button className="btn btn-primary btn-block submit-btn">Sign Up</button>
                  </form>
              </div>
          </div>
      );
}
