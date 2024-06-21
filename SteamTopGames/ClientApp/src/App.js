import React from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import SteamGames from './components/SteamGames';
import Sidebar from './components/Sidebar';
import LandingPage from './components/LandingPage';
import './App.css';

function App() {
    return (
        <Router>
            <div className="App">
                <Sidebar />
                <div className="content">
                    <Routes>
                        <Route path="/" element={<LandingPage />} />
                        <Route path="/steam-games" element={<SteamGames />} />
                        <Route path="*" element={<Navigate to="/" />} />
                    </Routes>
                </div>
            </div>
        </Router>
    );
}

export default App;
