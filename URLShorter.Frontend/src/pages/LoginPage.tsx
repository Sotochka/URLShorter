import React, { useState } from 'react';
import axios from '../services/api'; // API service

const LoginPage: React.FC = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const response = await axios.post('/auth/login', { email, password });
      localStorage.setItem('token', response.data.token); // Save token
      window.location.href = '/main'; // Redirect to Main Page
    } catch (error) {
      console.error('Login failed', error);
    }
  };

  return (
  <>
    <button onClick={() => window.location.href = '/main'} style={{marginRight: 10}}>Main Page</button>
    <button onClick={() => window.location.href = '/register'}>Register</button>
    <form onSubmit={handleLogin}>
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
