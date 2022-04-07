import { useEffect, useState } from "react";

export default function RegisterPage() {
  const [email, setEmail] = useState("");

  const register = async () => {
    const response = await fetch("https://localhost:7161/register", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email,
        password: "string",
        firstName: "string",
        lastName: "string",
      }),
    });
    if (response.status === 200) {
      const json = await response.json();
    }
  };

  return (
    <div>
      <input value={email} onChange={(event) => setEmail(event.target.value)} />
      <button onClick={register}>Register</button>
    </div>
  );
}
