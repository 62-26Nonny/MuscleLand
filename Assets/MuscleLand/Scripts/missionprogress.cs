using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class missionprogress : MonoBehaviour
{
  private string dbName = "URI=file:DB/server.db";
  private string dbNameC = "URI=file:DB/client.db";
  List<int> DQID = new List<int>();
  List<int> WQID = new List<int>();

  public List<GameObject> missionboxDaily;
  public List<GameObject> missionboxWeekly;
  void Start()
  {
    addQID();
    progresstext();
  }

  // Update is called once per frame
  void Update()
  {

  }

  void progresstext()
  {
    float Svalue;
    float times;
    bool claimed;
    string questdescription;
    int i;

    for (i = 0; i < missionboxDaily.Count; i++)
    {

      using (var conection = new SqliteConnection(dbName))
      {
        conection.Open();
        using (var command = conection.CreateCommand())
        {
          command.CommandText = "SELECT * FROM quest WHERE questID = '" + DQID[i] + "';";
          using (var reader = command.ExecuteReader())
          {
            times = float.Parse(reader["times"].ToString());
            questdescription = reader["description"].ToString();
          }
        }
        conection.Close();
      }

      using (var conection = new SqliteConnection(dbNameC))
      {
        conection.Open();
        using (var command = conection.CreateCommand())
        {
          command.CommandText = "SELECT * FROM dailyquest WHERE questID = '" + DQID[i] + "';";
          using (var reader = command.ExecuteReader())
          {
            claimed = (bool)reader["claimed"];
          }
        }
        conection.Close();
      }

      Svalue = missionboxDaily[i].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().value;
      missionboxDaily[i].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().maxValue = times;
      missionboxDaily[i].transform.Find("Missioninfo").gameObject.GetComponent<Text>().text = questdescription;


      if (Svalue < times)
      {
        missionboxDaily[i].transform.Find("progress Text").gameObject.GetComponent<Text>().text = Svalue.ToString() + "/" + times.ToString();
      }
      else
      {
        if (claimed)
        {
          missionboxDaily[i].transform.Find("progress Text").gameObject.SetActive(false);
          missionboxDaily[i].transform.Find("complete Text").gameObject.SetActive(true);
        }
        else
        {
          missionboxDaily[i].transform.Find("progress Text").gameObject.GetComponent<Text>().text = times.ToString() + "/" + times.ToString();
          missionboxDaily[i].transform.Find("Button").gameObject.SetActive(true);
        }
      }
    }


    for (i = 0; i < missionboxDaily.Count; i++)
    {
      using (var conection = new SqliteConnection(dbName))
      {
        conection.Open();
        using (var command = conection.CreateCommand())
        {
          command.CommandText = "SELECT * FROM quest WHERE questID = '" + WQID[i] + "';";
          using (var reader = command.ExecuteReader())
          {
            times = float.Parse(reader["times"].ToString());
            questdescription = reader["description"].ToString();
          }
        }
        conection.Close();
      }

      Svalue = missionboxWeekly[i].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().value;
      missionboxWeekly[i].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().maxValue = times;
      missionboxWeekly[i].transform.Find("Missioninfo").gameObject.GetComponent<Text>().text = questdescription;


      if (Svalue < times)
      {
        missionboxWeekly[i].transform.Find("progress Text").gameObject.GetComponent<Text>().text = Svalue.ToString() + "/" + times.ToString();
      }
      else
      {
        missionboxWeekly[i].transform.Find("progress Text").gameObject.GetComponent<Text>().text = times.ToString() + "/" + times.ToString();
        missionboxWeekly[i].transform.Find("Button").gameObject.SetActive(true);
      }
    }
  }

  public void addQID()
  {
    using (var conection = new SqliteConnection(dbNameC))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "SELECT * FROM dailyquest;";
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
            DQID.Add((int)reader["questID"]);
          reader.Close();
        }
        command.CommandText = "SELECT * FROM weeklyquest;";

        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
            WQID.Add((int)reader["questID"]);
          reader.Close();
        }

      }
      conection.Close();
    }
  }
}
