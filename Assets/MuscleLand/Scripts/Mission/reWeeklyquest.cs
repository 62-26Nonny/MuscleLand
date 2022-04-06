using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Mono.Data.Sqlite;

public class reWeeklyquest : MonoBehaviour
{
  List<int> numbers = new List<int>();
  List<int> QID = new List<int>();

  private void Start()
  {
    daycheck();
  }

  public void daycheck()
  {
    string startday;

    using (var conection = new SqliteConnection(Database.Instance.dbClient))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "SELECT * FROM weeklyquest;";
        using (var reader = command.ExecuteReader())
        {
          startday = reader["startdate"].ToString();
          reader.Close();
        }
      }
      conection.Close();
    }

    DateTime startdate = Convert.ToDateTime(startday);
    if (datetimer.currentdate.Date >= startdate.AddDays(7))
    {
      ProgressCheck();
      rndWeeklyquest();
    }
  }
  
  public void ProgressCheck(){
    using (var conection = new SqliteConnection(Database.Instance.dbClient))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "SELECT * FROM weeklyquest;";
        using (var reader = command.ExecuteReader())
        {
          foreach (var item in reader)
          {
            int questId = (int)reader["questID"];
            StartCoroutine(WebRequest.Instance.GetRequest("/quest/" + questId, (json) => 
            { 
              int weeklyprogress;
              int weeklygoal;
              //bool claimedweekly;
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

                if (weeklyprogress >= weeklygoal)
                {
                  WWWForm forms = new WWWForm();
                    StartCoroutine(WebRequest.Instance.PostRequest("/quest/complete/"+questId.ToString(), forms));
                }
              }));

            }));
            
          }
        }
      }
      conection.Close();
    }
  }

  public void rndWeeklyquest()
  {
    int range = 9;
    int quest;

    StartCoroutine(WebRequest.Instance.GetRequest("/quest/type/Weekly", (json) => 
    {
      QuestSerializer[] res = JsonHelper.getJsonArray<QuestSerializer>(json);
      foreach (var quest in res)
      {
        QID.Add(quest.questID);
      }

      for (int i = 0; i < 3; i++)
      {
        int questId = RandomNumber(range) + 1;

        WWWForm forms = new WWWForm();
        StartCoroutine(WebRequest.Instance.PostRequest("/quest/accept/"+questId.ToString(), forms));
      };

      for (int i = 0; i < numbers.Count; i++)
      {
        quest = i + 1;
        ResetWeeklyquest(QID[numbers[i]], quest);
      }

      WWWForm form = new WWWForm();
      form.AddField("weekly", 0);
      StartCoroutine(WebRequest.Instance.PostRequest("/dungeonstat/reset", form, (json) => 
      {
        missionprogress.Instance.progresstextweekly();
      }));
    }));
  }

  public void ResetWeeklyquest(int id, int questnum)
  {
    using (var conection = new SqliteConnection(Database.Instance.dbClient))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {

        command.CommandText = "UPDATE weeklyquest set questID = '" + id + "' where quest='" + questnum + "';";
        command.ExecuteNonQuery();

        command.CommandText = "UPDATE weeklyquest set claimed = '0' where quest='" + questnum + "';";
        command.ExecuteNonQuery();

        command.CommandText = "UPDATE weeklyquest set startdate = '" + datetimer.currentdate.Date + "' where quest='" + questnum + "';";
        command.ExecuteNonQuery();
      }
      conection.Close();
    }
  }

  public int RandomNumber(int range)
  {
    while (true)
    {
      int random = UnityEngine.Random.Range(0, range);
      if (!numbers.Contains(random))
      {
        numbers.Add(random);
        return random;
      }
    }
  }
}
