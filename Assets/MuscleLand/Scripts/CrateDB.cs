using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;
public class CrateDB : MonoBehaviour
{
  private string dbName = "URI=file:DB/server.db";
  public int Ep;
  public int gold;
  public Text Eptext;
  public Text goldtext;
  void Start()
  {
    //CreateDB();
    //Adduser();
    //deleteX(2);
    //updateuser(1,"pa1");
    EPandgold();
  }


  public void EPandgold()
  {
    using (var conection = new SqliteConnection(dbName))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "SELECT * FROM User WHERE ID=1;";
        using (var reader = command.ExecuteReader())
        {
          Ep = (int)reader.GetValue(7);
          Debug.Log(Ep);
          gold = (int)reader.GetValue(8);
          Debug.Log(gold);
          reader.Close();
        }
      }

      conection.Close();
    }

  }
  public void CreateDB()
  {
    using (var conection = new SqliteConnection(dbName))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "CREATE TABLE IF NOT EXISTS XXX (itemID INT, pic VARCHAR);";
        command.ExecuteNonQuery();
      }

      conection.Close();
    }
  }
  public void updateuser(int id, string Username)
  {
    using (var conection = new SqliteConnection(dbName))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "UPDATE User set username = '" + Username +"' where ID='" + id +"';";
        command.ExecuteNonQuery();
      }
      conection.Close();
    }
  }

  public void deleteX(int id) 
  {
    using (var conection = new SqliteConnection(dbName))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "DELETE FROM User WHERE ID= '" + id +"' ";
        command.ExecuteNonQuery();
        command.Dispose();
      }
      conection.Close();
    }
  }
  public void Adduser()
  {
    using (var conection = new SqliteConnection(dbName))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "INSERT INTO User (username,password,profilepic) VALUES ('HHH','1234','ka');";
        command.ExecuteNonQuery();
      }

      conection.Close();
    }
  }
  public void DisplayUser()
  {
    using (var conection = new SqliteConnection(dbName))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "SELECT * FROM USer;";
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
              Debug.Log("ID :" + reader["ID"] + "\tusername :" + reader["username"]);

          reader.Close();
        }
      }

      conection.Close();
    }
  }

  public void CreateUser()
  {
    using (var conection = new SqliteConnection(dbName))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "CREATE TABLE IF NOT EXISTS User (ID INT,username VARCHAR(10), password VARCHAR(15), caloriesburn INT, caloriesburn_daily INT, profilepic VARCHAR,EXP INT,EP INT,gold INT,last_active date);";
        command.ExecuteNonQuery();
      }

      conection.Close();
    }
  }


  void Update()
  {
    Eptext.text = Ep.ToString();
    goldtext.text = gold.ToString();
  }
}
