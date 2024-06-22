import React from 'react';
import { Nav } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import '../App.css';
import '@fortawesome/fontawesome-free/css/all.min.css';

const Sidebar = () => {
    return (
        <div className="sidebar">

            <h2 className="mt-5">Top 10's</h2>
            <Nav className="flex-column">
                <Nav.Link as={Link} to="/"><i className="fas fa-home"></i> Home</Nav.Link>
                <Nav.Link as={Link} to="/steam-games"><i class="fa-solid fa-gamepad me-2"></i>Steam's Top 10 Games</Nav.Link>
                <Nav.Link as={Link} to="/top-songs"><i class="fa-solid fa-headphones me-2"></i>Spotify's Top 10 Songs</Nav.Link>
            </Nav>
        </div>
    );
};

export default Sidebar;