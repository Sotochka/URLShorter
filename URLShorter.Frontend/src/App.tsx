import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import MainPage from './pages/MainPage';
import MoreInfoPage from './pages/MoreInfoPage';

const App: React.FC = () => {
  const isAuthenticated = !!localStorage.getItem('token'); // Check if user is logged in

  return (
    <Router>
      <Routes>
        <Route path="/" element={<Navigate to="/main" />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/main" element={<MainPage />} />
        <Route
          path="/more-info/:id"
          element={isAuthenticated ? <MoreInfoPage /> : <Navigate to="/login" />}
        />
      </Routes>
    </Router>
  );
};

export default App;
