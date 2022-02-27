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
  private int rnd;
  string lastdatetime;
  string datetime;
  List<int> numbers = new List<int>();
  List<int> QID = new List<int>();


  // Start is called before the first frame update
  void Start()
  {
    DateTime utcDate = DateTime.UtcNow.Date;
    datetime = utcDate.ToString();
    //lastactive();
    //daycheck();
    rndDailyquest();
  }
  public void daycheck()
  {
    //lastdatecheck();
    DateTime utcDate = DateTime.UtcNow;
    datetime = utcDate.ToString();
    DateTime test = Convert.ToDateTime(lastdatetime);
    //rndDailyquest();
    //if(test.Day != utcDate.Day || test.Month != utcDate.Month || test.Year != utcDate.Year)
    //{
    //  Debug.Log("re");
    //  rndDailyquest();
    //}
    //else
    //{
    //  Debug.Log("non");
    //}
  }
  public void lastactive()
  {
    using (var conection = new SqliteConnection(dbName))
    {
      //int sumquest;
      conection.Open();
      using (var command = conection.CreateCommand())
      {

        command.CommandText = "UPDATE User set last_active = '" + datetime + "' where ID='1';";
        command.ExecuteNonQuery();
      }
      conection.Close();
    }
  }

  public void lastdatecheck()
  {
    using (var conection = new SqliteConnection(dbName))
    {
      //int sumquest;
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "SELECT * FROM User WHERE ID == '1';";
        using (var reader = command.ExecuteReader())
        {
          lastdatetime = reader["last_active"].ToString();
          reader.Close();
        }
      }
      conection.Close();
    }
  }
  // Update is called once per frame
  void Update()
  {
  }

  public void rndDailyquest()
  {
    int count;
    int x;
    using (var conection = new SqliteConnection(dbName))
    {
      //int sumquest;
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
        int i;
        for (i = 0; i < 3; i++)
        {
          rnd = NewNumber(count);
        };

        for (i = 0; i < numbers.Count; i++)
        {
          x = i + 1;
          resetDailyquest(QID[numbers[i]], x);
        }
      }
    }
  }

  public void resetDailyquest(int id,int questnum)
  {
    using (var conection = new SqliteConnection(dbNameC))
    {
      //int sumquest;
      conection.Open();
      using (var command = conection.CreateCommand())
      {

        command.CommandText = "UPDATE dailyquest set questID = '" + id + "' where quest='" + questnum + "';";
        command.ExecuteNonQuery();

        command.CommandText = "UPDATE dailyquest set claimed = '0' where quest='" + questnum + "';";
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
