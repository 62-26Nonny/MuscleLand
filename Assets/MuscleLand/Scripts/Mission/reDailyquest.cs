using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Mono.Data.Sqlite;

public class reDailyquest : MonoBehaviour
{
  private string dbClient = "URI=file:DB/client.db";
  List<int> numbers = new List<int>();
  List<int> QID = new List<int>();

  private void Start()
  {
    daycheck();
  }

  public void daycheck()
  {
    string startday;

    using (var conection = new SqliteConnection(dbClient))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "SELECT * FROM dailyquest;";
        using (var reader = command.ExecuteReader())
        {
          startday = reader["startdate"].ToString();
          reader.Close();
        }
      }
      conection.Close();
    }

    DateTime startdate = Convert.ToDateTime(startday);

    if (datetimer.currentdate.Date > startdate)
    {
      ProgressCheck();
      rndDailyquest();
    }
  }

  public void ProgressCheck(){
    using (var conection = new SqliteConnection(dbClient))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "SELECT * FROM dailyquest;";
        using (var reader = command.ExecuteReader())
        {
          foreach (var item in reader)
          {
            int questId = (int)reader["questID"];
            StartCoroutine(WebRequest.Instance.GetRequest("/quest/" + questId, (json) => 
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

                if (dailyprogress >= dailygoal)
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
  public void rndDailyquest()
  {
    int range = 9;
    int quest;

    StartCoroutine(WebRequest.Instance.GetRequest("/quest/type/Daily", (json) => 
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
        ResetDailyquest(QID[numbers[i]], quest);
      }

      WWWForm form = new WWWForm();
      form.AddField("daily", 0);
      StartCoroutine(WebRequest.Instance.PostRequest("/dungeonstat/reset", form, (json) => 
      {
        missionprogress.Instance.progresstextdaily();
      }));

    }));
  }

  public void ResetDailyquest(int id, int questnum)
  {
    using (var conection = new SqliteConnection(dbClient))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {

        command.CommandText = "UPDATE dailyquest set questID = '" + id + "' where quest='" + questnum + "';";
        command.ExecuteNonQuery();

        command.CommandText = "UPDATE dailyquest set claimed = '0' where quest='" + questnum + "';";
        command.ExecuteNonQuery();

        command.CommandText = "UPDATE dailyquest set startdate = '" + datetimer.currentdate.Date + "' where quest='" + questnum + "';";
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
