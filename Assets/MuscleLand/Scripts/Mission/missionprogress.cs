using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class missionprogress : MonoBehaviour
{
  public static missionprogress Instance;
  [SerializeField] GameObject dailyQuetView;
  [SerializeField] GameObject weeklyQuetView;
  [SerializeField] GameObject Prefab_quest;
  List<int> DQID = new List<int>();
  List<int> WQID = new List<int>();
  public List<GameObject> missionboxDaily;
  public List<GameObject> missionboxWeekly;

  private void Start()
  {
    Instance = this;
    addmission();
    addQID();
    progresstextdaily();
    progresstextweekly();
  }

  public void progresstextdaily()
  {
    for (int i = 0; i < missionboxDaily.Count; i++)
    {
      StartCoroutine(WebRequest.Instance.GetRequest("/quest/" + DQID[i], (json) => 
      {
        int dailyprogress;
        int dailygoal;
        bool claimeddaily;
        string questDescription;
        string difficulty;
        int dungeonID;
        int questID;

        QuestSerializer[] res = JsonHelper.getJsonArray<QuestSerializer>(json);
        dailygoal = res[0].times;
        questDescription = res[0].description;
        difficulty = res[0].difficulty;
        dungeonID = res[0].dungeonID;
        questID = res[0].questID;

        StartCoroutine(WebRequest.Instance.GetRequest("/dungeonstat/" + Player.userID + "/" + dungeonID + "/" + difficulty, (json) => 
        {
          DungeonStatSerializer[] res = JsonHelper.getJsonArray<DungeonStatSerializer>(json);
          dailyprogress = res[0].daily;
          int questNum;

          using (var conection = new SqliteConnection(Database.Instance.dbClient))
          {
            conection.Open();
            using (var command = conection.CreateCommand())
            {
              command.CommandText = "SELECT * FROM dailyquest WHERE questID = '" + questID + "';";
              using (var reader = command.ExecuteReader())
              {
                questNum = int.Parse(reader["quest"].ToString()) - 1;
                claimeddaily = (bool)reader["claimed"];
              }
            }
            conection.Close();
          }

          missionboxDaily[questNum].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().value = dailyprogress;
          missionboxDaily[questNum].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().maxValue = dailygoal;
          missionboxDaily[questNum].transform.Find("Missioninfo").gameObject.GetComponent<Text>().text = questDescription;

          if (dailyprogress < dailygoal)
          {
            missionboxDaily[questNum].transform.Find("progress Text").gameObject.GetComponent<Text>().text = dailyprogress.ToString() + "/" + dailygoal.ToString();
          }
          else
          {
            if (claimeddaily)
            {
              missionboxDaily[questNum].transform.Find("progress Text").gameObject.SetActive(false);
              missionboxDaily[questNum].transform.Find("complete Text").gameObject.SetActive(true);
            }
            else
            {
              missionboxDaily[questNum].transform.Find("progress Text").gameObject.GetComponent<Text>().text = dailygoal.ToString() + "/" + dailygoal.ToString();
              missionboxDaily[questNum].transform.Find("Button").gameObject.SetActive(true);
            }
          }
        }));
      }));
    }
  }

  public void progresstextweekly()
  {
    for (int i = 0; i < missionboxWeekly.Count; i++)
    {
      StartCoroutine(WebRequest.Instance.GetRequest("/quest/" + WQID[i], (json) => 
      {
        int weeklyprogress;
        int weeklygoal;
        bool claimedweekly;
        string questDescription;
        string difficulty;
        int dungeonID;
        int questID;

        QuestSerializer[] res = JsonHelper.getJsonArray<QuestSerializer>(json);
        weeklygoal = res[0].times;
        questDescription = res[0].description;
        difficulty = res[0].difficulty;
        dungeonID = res[0].dungeonID;
        questID = res[0].questID;

        StartCoroutine(WebRequest.Instance.GetRequest("/dungeonstat/" + Player.userID + "/" + dungeonID + "/" + difficulty, (json) => 
        {
          DungeonStatSerializer[] res = JsonHelper.getJsonArray<DungeonStatSerializer>(json);
          weeklyprogress = res[0].weekly;
          int questNum;

          using (var conection = new SqliteConnection(Database.Instance.dbClient))
          {
            conection.Open();
            using (var command = conection.CreateCommand())
            {
              command.CommandText = "SELECT * FROM weeklyquest WHERE questID = '" + questID + "';";
              using (var reader = command.ExecuteReader())
              {
                questNum = (int)reader["quest"] - 1;
                claimedweekly = (bool)reader["claimed"];
              }
            }
            conection.Close();
          }

          missionboxWeekly[questNum].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().value = weeklyprogress;
          missionboxWeekly[questNum].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().maxValue = weeklygoal;
          missionboxWeekly[questNum].transform.Find("Missioninfo").gameObject.GetComponent<Text>().text = questDescription;

          if (weeklyprogress < weeklygoal)
          {
            missionboxWeekly[questNum].transform.Find("progress Text").gameObject.GetComponent<Text>().text = weeklyprogress.ToString() + "/" + weeklygoal.ToString();
          }
          else
          {
            if (claimedweekly)
            {
              missionboxWeekly[questNum].transform.Find("progress Text").gameObject.SetActive(false);
              missionboxWeekly[questNum].transform.Find("complete Text").gameObject.SetActive(true);
            }
            else
            {
              missionboxWeekly[questNum].transform.Find("progress Text").gameObject.GetComponent<Text>().text = weeklygoal.ToString() + "/" + weeklygoal.ToString();
              missionboxWeekly[questNum].transform.Find("Button").gameObject.SetActive(true);
            }
          }
        }));
      }));
    }
  }

  public void addQID()
  {
    using (var conection = new SqliteConnection(Database.Instance.dbClient))
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
    using (var conection = new SqliteConnection(Database.Instance.dbClient))
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