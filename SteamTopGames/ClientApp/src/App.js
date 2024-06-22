import React, { useState } from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import SteamGames from './components/SteamGames';
import Sidebar from './components/Sidebar';
import LandingPage from './components/LandingPage';
import { GiHamburgerMenu } from 'react-icons/gi';
import './App.css';

function App() {
    const [showBar, setShowBar] = useState(false);

    return (
        <Router>
            <div className="App">
                <div className="hamburger-menu">
                    <GiHamburgerMenu onClick={() => setShowBar(!showBar)} size={30} />
                </div>

                {showBar && <Sidebar />}

                <div className={`content ${showBar ? 'shifted' : ''}`}>
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
