import {
  BarChart,
  Bar,
  Cell,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  ResponsiveContainer,
} from "recharts";
import "./barChart.css";

export default function MyBarChart({ data, title }) {
  return (
    <div className="barChart">
      <h3 className="barChartTitle">{title}</h3>
      <BarChart width={600} height={300} data={data}>
        <CartesianGrid strokeDasharray="3 3" />
        <XAxis dataKey="Km" />
        <YAxis dataKey="Today" />
        <Tooltip />
        <Legend />
        <Bar dataKey="Km" fill="#82ca9d" />
      </BarChart>
    </div>
  );
}
