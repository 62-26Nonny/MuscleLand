import React from "react";
import "./sidebar.css";
import { Link } from "react-router-dom";
export default function Sidebar() {
  return (
    <div className="sidebar">
      <div className="sidebarWrapper">
        <h3 className="sidebarTitle">Dashboard</h3>
        <ul className="sidebarList">
          <li className="sidebarItem">
            <Link to="/users">Users</Link>
          </li>
          <li className="sidebarItem">
            <Link to="/dungeon">Dungeon</Link>
          </li>
          <li className="sidebarItem">
            <Link to="/exploration">Exploration</Link>
          </li>
          <li className="sidebarItem">
            <Link to="/mission">Mission</Link>
          </li>
          <li className="sidebarItem">
            <Link to="/shop">Shop</Link>
          </li>
        </ul>
      </div>
    </div>
  );
}
