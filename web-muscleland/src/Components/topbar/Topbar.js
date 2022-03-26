import React from "react";
import "./topbar.css";
import { Button } from "react-bootstrap";

export default function MyTopbar() {
  return (
    <div className="topbar">
      <div className="topbarWrapper">
        <div className="topLeft">
          <span className="logo">MUSCLE LAND</span>
        </div>
        <div className="topRight">
          <div className="topbarContainer">
            <span className="userName">User: PETCHJA</span>
            <Button variant="light">Log in</Button>
          </div>
        </div>
      </div>
    </div>
  );
}
