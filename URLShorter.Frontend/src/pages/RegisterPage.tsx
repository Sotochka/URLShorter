import React, { useState } from 'react';
import axios from '../services/api'; // API service

const LoginPage: React.FC = () => {
  const [userName, setUserName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const response = await axios.post('/auth/register', {userName, email, password });
      window.location.reload();
      localStorage.setItem('token', response.data.token); // Save token
      window.location.href = '/main'; // Redirect to Main Page
    } catch (error) {
      console.error('Login failed', error);
    }
  };

  return (
    <>
    <button onClick={() => window.location.href = '/main'} style={{
      marginRight: '10px'
    }}>Main Page</button>
    <button onClick={() => window.location.href = '/login'}>Register</button>
    <form onSubmit={handleLogin}>
      <h2>Register</h2>
      <input
        type="text"
        placeholder="UserName"
        value={userName}
        onChange={(e) => setUserName(e.target.value)}
        required
        />
      <input
        type="email"
        placeholder="Email"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        required
        />
      <input
        type="password"
        placeholder="Password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        required
        />
      <button type="submit">Login</button>
    </form>
  </>
  );
};

export default LoginPage;
