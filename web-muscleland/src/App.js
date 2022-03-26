import "bootstrap/dist/css/bootstrap.min.css";
import Topbar from "./Components/topbar/Topbar";
import Sidebar from "./Components/sidebar/Sidebar";
import "./app.css";
import Users from "./Components/pages/user/Users.js";
import Dungeon from "./Components/pages/dungeon/Dungeon";
import Exploration from "./Components/pages/exploration/Exploration";
import Mission from "./Components/pages/mission/Mission";
import Shop from "./Components/pages/shop/Shop";
import RisingTab from "./Components/dungeonTab/risingKnee/RisingKnee";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

function App() {
  return (
    <Router>
      <Topbar />
      <div className="container">
        <Sidebar />
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

export default App;
