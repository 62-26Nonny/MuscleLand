using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class missionprogress : MonoBehaviour
{
  private string dbName = "URI=file:DB/server.db";
  private string dbNameC = "URI=file:DB/client.db";
  string userid = Player.userID;
  //private string userid = "1";
  public static missionprogress Instance;
  [SerializeField] GameObject dailyQuetView;
  [SerializeField] GameObject weeklyQuetView;
  [SerializeField] GameObject Prefab_quest;
  List<int> DQID = new List<int>();
  List<int> WQID = new List<int>();

  public List<GameObject> missionboxDaily;
  public List<GameObject> missionboxWeekly;

  void Start()
  {
    Instance = this;
    addmission();
    addQID();
    progresstextdaily();
    progresstextweekly();
  }

  public void progresstextdaily()
  {
    float dailyprogress;
    float dailygoal;
    bool claimeddaily;
    string questdescription;
    string difficulty;
    int dunID;
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
            dailygoal = (int)reader["times"];
            questdescription = reader["description"].ToString();
            difficulty = reader["difficulty"].ToString();
            dunID = (int)reader["dungeonID"];
          }

          command.CommandText = "SELECT * FROM dungeonstat WHERE userID = '" + userid + "' AND difficulty = '" + difficulty + "' AND dungeonID = '" + dunID + "';";
          using (var reader = command.ExecuteReader())
          {
            dailyprogress = (int)reader["daily"];
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
            claimeddaily = (bool)reader["claimed"];
          }
        }
        conection.Close();
      }

      missionboxDaily[i].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().value = dailyprogress;
      missionboxDaily[i].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().maxValue = dailygoal;
      missionboxDaily[i].transform.Find("Missioninfo").gameObject.GetComponent<Text>().text = questdescription;

      if (dailyprogress < dailygoal)
      {
        missionboxDaily[i].transform.Find("progress Text").gameObject.GetComponent<Text>().text = dailyprogress.ToString() + "/" + dailygoal.ToString();
      }
      else
      {
        if (claimeddaily)
        {
          missionboxDaily[i].transform.Find("progress Text").gameObject.SetActive(false);
          missionboxDaily[i].transform.Find("complete Text").gameObject.SetActive(true);
        }
        else
        {
          missionboxDaily[i].transform.Find("progress Text").gameObject.GetComponent<Text>().text = dailygoal.ToString() + "/" + dailygoal.ToString();
          missionboxDaily[i].transform.Find("Button").gameObject.SetActive(true);
        }
      }
    }
  }

  public void progresstextweekly()
  {
    float weeklyprogress = 1;
    float weeklygoal;
    bool claimeweekly;
    string questdescription;
    string difficulty;
    int dunID;
    int i;

    for (i = 0; i < missionboxWeekly.Count; i++)
    {
      using (var conection = new SqliteConnection(dbName))
      {
        conection.Open();
        using (var command = conection.CreateCommand())
        {
          command.CommandText = "SELECT * FROM quest WHERE questID = '" + WQID[i] + "';";
          using (var reader = command.ExecuteReader())
          {
            weeklygoal = float.Parse(reader["times"].ToString());
            questdescription = reader["description"].ToString();
            difficulty = reader["difficulty"].ToString();
            dunID = (int)reader["dungeonID"];
          }

          command.CommandText = "SELECT * FROM dungeonstat WHERE userID = '" + userid + "' AND difficulty = '" + difficulty + "' AND dungeonID = '" + dunID + "';";
          using (var reader = command.ExecuteReader())
          {
            weeklyprogress = (int)reader["weekly"];
          }

        }
        conection.Close();
      }

      using (var conection = new SqliteConnection(dbNameC))
      {
        conection.Open();
        using (var command = conection.CreateCommand())
        {
          command.CommandText = "SELECT * FROM weeklyquest WHERE questID = '" + WQID[i] + "';";
          using (var reader = command.ExecuteReader())
          {
            claimeweekly = (bool)reader["claimed"];
          }
        }
        conection.Close();
      }

      missionboxWeekly[i].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().value = weeklyprogress;
      missionboxWeekly[i].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().maxValue = weeklygoal;
      missionboxWeekly[i].transform.Find("Missioninfo").gameObject.GetComponent<Text>().text = questdescription;


      if (weeklyprogress < weeklygoal)
      {
        missionboxWeekly[i].transform.Find("progress Text").gameObject.GetComponent<Text>().text = weeklyprogress.ToString() + "/" + weeklygoal.ToString();
      }
      else
      {
        if (claimeweekly)
        {
          missionboxWeekly[i].transform.Find("progress Text").gameObject.SetActive(false);
          missionboxWeekly[i].transform.Find("complete Text").gameObject.SetActive(true);
        }
        else
        {
          missionboxWeekly[i].transform.Find("progress Text").gameObject.GetComponent<Text>().text = weeklygoal.ToString() + "/" + weeklygoal.ToString();
          missionboxWeekly[i].transform.Find("Button").gameObject.SetActive(true);
        }
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
  public void addmission()
  {
    using (var conection = new SqliteConnection(dbNameC))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "SELECT * FROM dailyquest;";
        using (var reader = command.ExecuteReader())
        {
          foreach (var item in reader)
          {

            GameObject Clone = Instantiate(Prefab_quest);

            Clone.SetActive(true);
            Clone.name = reader["quest"].ToString();
            Clone.transform.SetParent(dailyQuetView.transform, false);

            Transform CloneTran = Clone.transform;

            missionboxDaily.Add(Clone);
          }

        }

        command.CommandText = "SELECT * FROM weeklyquest;";
        using (var reader = command.ExecuteReader())
        {
          foreach (var item in reader)
          {

            GameObject Clone = Instantiate(Prefab_quest);

            Clone.SetActive(true);
            Clone.name = reader["quest"].ToString();

            Clone.transform.SetParent(weeklyQuetView.transform, false);

            Transform CloneTran = Clone.transform;

            missionboxWeekly.Add(Clone);
          }

        }
        conection.Close();
      }
    }
  }


}