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
      rndDailyquest();
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
        RandomNumber(range);
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

  public void RandomNumber(int range)
  {
    while (true)
    {
      int random = UnityEngine.Random.Range(0, range);
      if (!numbers.Contains(random))
      {
        numbers.Add(random);
        return;
      }
    }
  }
}
