import "./dungeon.css";
import Tab from "../../dungeonTab/Layout.js";
import PieChart from "../../chart/PieChart";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Total from "../../dungeonTab/total/Total";

export default function Dungeon() {
  return (
    <Router>
      <div className="dungeonTitle">
        <Switch>
          <Route path="/total">
            <Total />
          </Route>
        </Switch>
        <Tab />
        {/* <PieChart /> */}
      </div>
    </Router>
  );
}
