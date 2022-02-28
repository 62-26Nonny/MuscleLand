using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Mono.Data.Sqlite;

public class reDailyquest : MonoBehaviour
{
  private string dbName = "URI=file:DB/server.db";
  private string dbNameC = "URI=file:DB/client.db";
  List<int> numbers = new List<int>();
  List<int> QID = new List<int>();


  // Start is called before the first frame update
  void Start()
  {
    daycheck();
    //rndDailyquest();
  }
  public void daycheck()
  {
    string startday;

    using (var conection = new SqliteConnection(dbNameC))
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
      Debug.Log("reset");
      rndDailyquest();
    }
  }
  void Update()
  {
  }

  public void rndDailyquest()
  {
    int count;
    int quest;
    int rnd;
    int i;

    using (var conection = new SqliteConnection(dbName))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {

        command.CommandText = "SELECT questID FROM quest WHERE type == 'Daily';";
        using (var reader = command.ExecuteReader())
        {
         while (reader.Read())
            QID.Add(int.Parse(reader["questID"].ToString()));
          reader.Close();
        }

        command.CommandText = "SELECT COUNT(questID) FROM quest WHERE type == 'Daily';";
        using (var reader = command.ExecuteReader())
        {
          count = int.Parse(reader["COUNT(questID)"].ToString());
          reader.Close();
        }
        conection.Close();
      }
    }

    for (i = 0; i < 3; i++)
    {
      rnd = NewNumber(count);
    };

    for (i = 0; i < numbers.Count; i++)
    {
      quest = i + 1;
      ResetDailyquest(QID[numbers[i]], quest);
    }

  }
  public void ResetDailyquest(int id,int questnum)
  {
    using (var conection = new SqliteConnection(dbNameC))
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
  public int NewNumber(int r)
  {
    int a = 0;

    while (a == 0)
    {
      a = UnityEngine.Random.Range(0, r-1);
      if (!numbers.Contains(a))
      {
        numbers.Add(a);
      }
      else
      {
        a = 0;
      }
    }
    return a;
  }

}
