import axios from 'axios';

const instance = axios.create({
  baseURL: 'http://localhost:5198/', // Your API base URL
  headers: {
    Authorization: `Bearer ${localStorage.getItem('token')}` // Attach token for authenticated requests
  }
});

export default instance;
