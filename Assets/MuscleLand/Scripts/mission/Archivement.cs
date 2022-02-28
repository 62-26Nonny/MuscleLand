using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class Archivement : MonoBehaviour
{
  private string dbName = "URI=file:DB/server.db";
  public int userID = 1;
  public List<GameObject> arcbox;
  void Start()
  {
    
  }

  // Update is called once per frame
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
            dunID = (int)reader["dubgeonsID"];
          }
          command.CommandText = "SELECT * FROM userachievement WHERE userID == '" + userID + "' AND arcID = '" + arcID + "';";
          using (var reader = command.ExecuteReader())
          {
            level = (int)reader["curlvl"];
          }

          command.CommandText = "SELECT SUM(total) FROM dungeonstat WHERE userID = '" + userID + "' AND dubgeonsID = '" + dunID + "';";
          using (var reader = command.ExecuteReader())
          {
            progress = float.Parse(reader["SUM(total)"].ToString());
          }

          command.CommandText = "SELECT SUM(fail) FROM dungeonstat WHERE userID = '" + userID + "' AND dubgeonsID = '" + dunID + "';";
          using (var reader = command.ExecuteReader())
          {
            progress = progress - float.Parse(reader["SUM(fail)"].ToString());
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
