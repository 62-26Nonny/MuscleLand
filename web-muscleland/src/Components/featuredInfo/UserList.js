import { DataGrid } from "@mui/x-data-grid";
import "./userList.css";

const columns = [
  { field: "id", headerName: "ID", width: 170 },
  { field: "userName", headerName: "User Name", width: 220 },
  { field: "status", headerName: "Status", width: 130 },
];

const rows = [
  { id: 1, userName: "Snow", status: "Online" },
  { id: 2, userName: "Lannister", status: "Offline" },
  { id: 3, userName: "Lannister", status: "Online" },
  { id: 4, userName: "Stark", status: "Online" },
  { id: 5, userName: "Targaryen", status: "Offline" },
  { id: 6, userName: "Melisandre", status: "Offline" },
  { id: 7, userName: "Clifford", status: "Online" },
  { id: 8, userName: "Frances", status: "Online" },
  { id: 9, userName: "Roxie", status: "Offline" },
];

export default function UserList() {
  return (
    <div className="table">
      <div style={{ height: 400, width: "95%" }}>
        <DataGrid
          rows={rows}
          columns={columns}
          pageSize={5}
          checkboxSelection
        />
      </div>
    </div>
  );
}
