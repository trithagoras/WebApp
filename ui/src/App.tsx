import React from 'react';
import logo from './logo.svg';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import Cookies from 'js-cookie'
import axios from 'axios';
import './App.css';
import Login from './pages/Login';
import MainApp from './pages/MainApp';

function App() {
  axios.defaults.baseURL = 'http://localhost:47642';
  const authed = () => Cookies.get("WebApp") !== undefined;

  function RequireAuth({children}: {children: JSX.Element}) {
    return authed() === true ? children : <Navigate to="/login" replace />;
  }

  return (
    <BrowserRouter>
      <Routes>
          <Route index element={
            <RequireAuth>
              <MainApp />
            </RequireAuth>
          } />
        <Route path="/login" element={<Login />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
