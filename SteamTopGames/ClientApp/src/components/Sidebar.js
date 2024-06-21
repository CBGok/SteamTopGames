import React from 'react';
import { Nav } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import '../App.css';

const Sidebar = () => {
    return (
        <div className="sidebar">
            <h2>Steam Games Info</h2>
            <Nav className="flex-column">
                <Nav.Link as={Link} to="/">Home</Nav.Link>
                <Nav.Link as={Link} to="/steam-games">Steam's Top 10 Games</Nav.Link>
            </Nav>
        </div>
    );
};

export default Sidebar;