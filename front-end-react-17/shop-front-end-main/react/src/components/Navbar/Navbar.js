import React, { useState } from 'react';
import { MenuItems, MenuItemsLoggedInUser, MenuItemsLoggedInAdmin } from "./MenuItems";
import './Navbar.css';

export default function NavBar(props) {
    //state = { clicked: false }
    const [clicked, setClicked] = useState(0);

    function handleClick() {
        setClicked(!clicked);
    }

    //TODO: ADD ROLES, AND CHANGE NAVBAR ACCORDING TO STUDENT/TEACHER
    let buttons;
    if (props.user) {
            buttons = (
                <ul className={clicked ? 'nav-menu active' : 'nav-menu'}>
                    {MenuItemsLoggedInUser.map((item, index) => {
                        return (
                            <li key={index}>
                                <a className={item.cName} href={item.url} >
                                    {item.title}
                                </a>
                            </li>
                        )
                    })}
                </ul>
            )
        }
    else {
        buttons = (
            <ul className={clicked ? 'nav-menu active' : 'nav-menu'}>
                {MenuItems.map((item, index) => {
                    return (
                        <li key={index}>
                            <a className={item.cName} href={item.url} >
                                {item.title}
                            </a>
                        </li>
                    )
                })}
            </ul>
        )

    }
    return (
        <nav className="NavbarItems">
            <h1 className="navbar-logo">Academic Info<i className="fab fa-react"></i></h1>
            <div className="menu-icon" onClick={handleClick}>
                <i className={clicked ? 'fas fa-times' : 'fas fa-bars'}></i>
            </div>
            {buttons}
        </nav>
    )
}