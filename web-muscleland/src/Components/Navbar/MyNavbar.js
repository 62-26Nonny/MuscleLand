import React, { Component } from "react";
import { Navbar, Nav, Container, Offcanvas, Button } from "react-bootstrap";
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";
import Users from "../pages/Users";
import Dungeon from "../pages/Dungeon";
import Exploration from "../pages/Exploration";
import Mission from "../pages/Mission";
import Shop from "../pages/Shop";
export default class MyNavbar extends Component {
  render() {
    return (
      <Router>
        <div>
          <Navbar bg="light" expand={false}>
            <Container fluid>
              <Navbar.Toggle aria-controls="offcanvasNavbar" />
              <Navbar.Offcanvas
                id="offcanvasNavbar"
                aria-labelledby="offcanvasNavbarLabel"
                placement="start"
              >
                <Offcanvas.Header closeButton>
                  <Offcanvas.Title id="offcanvasNavbarLabel">
                    Manu
                  </Offcanvas.Title>
                </Offcanvas.Header>
                <Offcanvas.Body>
                  <Nav className="justify-content-end flex-grow-1 pe-3">
                    <Nav.Link as={Link} to={"/users"}>
                      Users
                    </Nav.Link>
                    <Nav.Link as={Link} to={"/dungeon"}>
                      Dungeon
                    </Nav.Link>
                    <Nav.Link as={Link} to={"/exploration"}>
                      Exploration
                    </Nav.Link>
                    <Nav.Link as={Link} to={"/mission"}>
                      Mission
                    </Nav.Link>
                    <Nav.Link as={Link} to={"/shop"}>
                      Shop
                    </Nav.Link>
                  </Nav>
                </Offcanvas.Body>
              </Navbar.Offcanvas>
              <Navbar.Brand href="#">Muscle Land</Navbar.Brand>
              <Button variant="primary">Log in</Button>{" "}
            </Container>
          </Navbar>
        </div>
        <div>
          <Switch>
            <Route path="/users">
              <Users />
            </Route>
            <Route path="/dungeon">
              <Dungeon />
            </Route>
            <Route path="/exploration">
              <Exploration />
            </Route>
            <Route path="/mission">
              <Mission />
            </Route>
            <Route path="/shop">
              <Shop />
            </Route>
          </Switch>
        </div>
      </Router>
    );
  }
}
