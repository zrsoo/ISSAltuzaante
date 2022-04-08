import { useEffect, useState } from "react";

export default function RegisterPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [city, setCity] = useState("");
  const [year, setYear] = useState("");
  const register = async () => {
    const response = await fetch("https://localhost:7161/register", {
      method: "POST",
      mode: "cors",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email,
        password,
        firstName,
        lastName,
        city,
        year: parseInt(year),
      }),
    });
    if (response.status === 200) {
      const json = await response.json();
    }
  };

  return (
    <div>
      <label htmlFor="emailInput">Email</label>
      <input
        id="emailInput"
        value={email}
        onChange={(event) => setEmail(event.target.value)}
      />
      <br />
      <label htmlFor="passwordInput">Password</label>
      <input
        id="passwordInput"
        value={password}
        type="password"
        onChange={(event) => setPassword(event.target.value)}
      />
      <br />
      <label htmlFor="firstNameInput">First Name</label>
      <input
        id="firstNameInput"
        value={firstName}
        onChange={(event) => setFirstName(event.target.value)}
      />
      <br />
      <label htmlFor="lastNameInput">Last Name</label>
      <input
        id="lastNameInput"
        value={lastName}
        onChange={(event) => setLastName(event.target.value)}
      />
      <br />
      <label htmlFor="CityInput">City</label>
      <input
        id="CityInput"
        value={city}
        onChange={(event) => setCity(event.target.value)}
      />
      <br />
      <label htmlFor="yearInput">Year</label>
      <input
        id="yearInput"
        value={year}
        onChange={(event) => setYear(event.target.value)}
      />
      <button onClick={register}>Register</button>
    </div>
  );
}
