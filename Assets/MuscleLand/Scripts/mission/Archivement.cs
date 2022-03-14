using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class Archivement : MonoBehaviour
{
  private string dbName = "URI=file:DB/server.db";
  public List<GameObject> arcbox;

  void Update()
  {
    progresstext();
  }

  void progresstext()
  {
    float progress;
    float goal;
    string arc;
    int level;
    float cerrentgoal;
    string difficulty;
    int dunID;
    int i;
    int arcID;

    for (i = 0; i < arcbox.Count; i++)
    {
      arcID = i + 1;
      using (var conection = new SqliteConnection(dbName))
      {
        conection.Open();
        using (var command = conection.CreateCommand())
        {
          command.CommandText = "SELECT * FROM achievement WHERE arcID = '" + arcID + "';";
          using (var reader = command.ExecuteReader())
          {
            goal = float.Parse(reader["times"].ToString());
            arc = reader["arcname"].ToString();
            dunID = (int)reader["dungeonID"];
          }
          command.CommandText = "SELECT * FROM userachievement WHERE userID == '" + Player.userID + "' AND arcID = '" + arcID + "';";
          using (var reader = command.ExecuteReader())
          {
            level = (int)reader["curlvl"];
          }

          command.CommandText = "SELECT SUM(total) FROM dungeonstat WHERE userID = '" + Player.userID + "' AND dungeonID = '" + dunID + "';";
          using (var reader = command.ExecuteReader())
          {
            progress = float.Parse(reader["SUM(total)"].ToString());
          }

        }
        conection.Close();
      }

      cerrentgoal = goal * level;
      arcbox[i].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().value = progress;
      arcbox[i].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().maxValue = cerrentgoal;
      arcbox[i].transform.Find("Archivement").gameObject.GetComponent<Text>().text = arc;
      arcbox[i].transform.Find("Archivement info").gameObject.GetComponent<Text>().text = "Play " + arc + " " + cerrentgoal.ToString() + " time";

      if (progress < cerrentgoal)
      {
        arcbox[i].transform.Find("progress Text").gameObject.GetComponent<Text>().text = progress.ToString() + "/" + cerrentgoal.ToString();
      }
      else
      {
        if (level > 3)
        {
          arcbox[i].transform.Find("progress Text").gameObject.GetComponent<Text>().text = cerrentgoal.ToString() + "/" + cerrentgoal.ToString();
          arcbox[i].transform.Find("complete Text").gameObject.SetActive(true);
        }
        else
        {
          arcbox[i].transform.Find("progress Text").gameObject.GetComponent<Text>().text = cerrentgoal.ToString() + "/" + cerrentgoal.ToString();
          arcbox[i].transform.Find("Button").gameObject.SetActive(true);
        }
      }
    }
  }
}
