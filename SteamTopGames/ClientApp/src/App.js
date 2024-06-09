import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import SteamGames from './components/SteamGames';
import './App.css';

function App() {
    return (
        <Router>
            <div className="App">
                <Routes>
                    <Route path="/steam-games" element={<SteamGames />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
