import "./users.css";
import UserInfo from "../../featuredInfo/UserInfo.js";
import UserList from "../../featuredInfo/UserList.js";
import Chart from "../../chart/Chart";
import { userData } from "../../../data/userData";

export default function Users() {
  return (
    <div>
      <div className="users">
        <UserInfo />
        <Chart data={userData} title="Actvie Users in 1 month" grid />
        <UserList />
      </div>
    </div>
  );
}
