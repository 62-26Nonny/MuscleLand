import MissionInfo from "../../featuredInfo/MissionInfo";
import "./mission.css";
import MissionList from "../../featuredInfo/MissionList";
export default function Mission() {
  return (
    <div className="mission">
      <MissionInfo />
      <MissionList />
    </div>
  );
}
