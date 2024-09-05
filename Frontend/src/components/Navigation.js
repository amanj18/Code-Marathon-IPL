
import React from 'react';
import { Link } from 'react-router-dom'; 
import '../styles/Navbar.css';

const Navigation = () => {
    return (
        <nav className="navbar">
            <div className="navbar-brand">
                <Link to="/" className="navbar-logo">MySite</Link>
            </div>
            <ul className="navbar-menu">
                <li><Link to="/" className="navbar-item button-link">Home</Link></li>
                <li><Link to="/Post" className="navbar-item button-link" >Add Player </Link></li>
                <li><Link to="/Match-Details" className="navbar-item button-link" > Match Details</Link></li>
                <li><Link to="/top-players" className="navbar-item button-link" >Top players</Link></li>
                <li><Link to="/date-range" className="navbar-item button-link" >Date Range</Link></li>
            </ul>
        </nav>
    );
};

export default Navigation;
