using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class reWeeklyquest : MonoBehaviour
{
  private string dbName = "URI=file:DB/server.db";
  private string dbNameC = "URI=file:DB/client.db";
  private int rnd;
  int count;
  int x;
  List<int> numbers = new List<int>();
  List<int> QID = new List<int>();


  // Start is called before the first frame update
  void Start()
  {
    rndWeeklyquest();
  }

  // Update is called once per frame
  void Update()
  {
  }
  public void rndWeeklyquest()
  {
    using (var conection = new SqliteConnection(dbName))
    {
      //int sumquest;
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "SELECT questID FROM quest WHERE type == 'Weekly';";
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
            QID.Add(int.Parse(reader["questID"].ToString()));
          reader.Close();
        }

        command.CommandText = "SELECT COUNT(questID) FROM quest WHERE type == 'Weekly';";
        using (var reader = command.ExecuteReader())
        {
          count = int.Parse(reader["COUNT(questID)"].ToString());
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
          resetWeeklyquest(QID[numbers[i]], x);
        }
      }
    }
  }

  public void resetWeeklyquest(int id, int questnum)
  {
    using (var conection = new SqliteConnection(dbNameC))
    {
      //int sumquest;
      conection.Open();
      using (var command = conection.CreateCommand())
      {

        command.CommandText = "UPDATE weeklyquest set questID = '" + id + "' where quest='" + questnum + "';";
        command.ExecuteNonQuery();

        command.CommandText = "UPDATE weeklyquest set claimed = 'false' where quest='" + questnum + "';";
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
      a = Random.Range(0, r - 1);
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
