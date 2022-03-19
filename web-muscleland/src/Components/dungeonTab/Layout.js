import * as React from "react";
import { Tabs, Tab } from "@material-ui/core";
import Total from "./total/Total";
import Rising from "./risingKnee/RisingKnee";
import MyPieChart from "../chart/PieChart";
import { Link } from "react-router-dom";

export default function Mytabs() {
  const [selectedTab, setSelectedTab] = React.useState("1");

  const handleChange = (event, newValue) => {
    setSelectedTab(newValue);
  };

  return (
    <div>
      <Tabs value={selectedTab} onChange={handleChange}>
        <Tab label="Total" />
        <Tab label="Rising Knee" />
      </Tabs>
      {selectedTab === 0 && <Total />}
      {selectedTab === 1 && <Rising />}
    </div>
  );
}
