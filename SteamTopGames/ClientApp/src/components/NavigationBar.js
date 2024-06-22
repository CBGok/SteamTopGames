import React from 'react';
import { Navbar, Nav } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { GiHamburgerMenu } from 'react-icons/gi';
import '../App.css'; // CSS dosyanızın yolu doğru olduğundan emin olun

const NavigationBar = () => {
    return (
        <Navbar expand="lg" className="navbar-custom">
            <Navbar aria-controls="basic-navbar-nav">
                <GiHamburgerMenu />
            </Navbar>
            <Navbar.Collapse id="basic-navbar-nav">
                <Nav className="ml-auto">
                    <Nav.Link as={Link} to="/">Home</Nav.Link>
                    <Nav.Link as={Link} to="/steam-games">Steam's Top 10 Games</Nav.Link>
                </Nav>
            </Navbar.Collapse>
        </Navbar>
    );
};

export default NavigationBar;
