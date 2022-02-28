using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class claimebuttondaily : MonoBehaviour
{
  private string dbName = "URI=file:DB/server.db";
  private string dbNameC = "URI=file:DB/client.db";
  public Text XPtext;
  public Text GOLDtext;
  public int getXP;
  public int getGOLD;
  public int userID = 1;
  void Start()
  {

  }
  void Update()
  {

  }
  public void rewarding()
  {
    rewardcheck(int.Parse(this.transform.parent.gameObject.name));
    XPtext.text = getXP.ToString() + "XP";
    GOLDtext.text = getGOLD.ToString() + " GOLD";
    updateuser(userID);
    this.gameObject.SetActive(false);
  }

  public void rewardcheck(int quest)
  {
    int questID;
    using (var conection = new SqliteConnection(dbNameC))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "SELECT * FROM dailyquest WHERE quest = '" + quest + "';";
        using (var reader = command.ExecuteReader())
        {
          questID = (int)reader["questID"];
          reader.Close();
        }
      }
      conection.Close();
    }

    using (var conection = new SqliteConnection(dbName))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "SELECT * FROM quest WHERE questID = '" + questID + "';";
        using (var reader = command.ExecuteReader())
        {
          getXP = (int)reader["EXP"];
          getGOLD = (int)reader["GOLD"];
          reader.Close();
        }
      }
      conection.Close();
    }
    updateclaimed(quest);
  }

  public void updateclaimed(int quest)
  {

    using (var conection = new SqliteConnection(dbNameC))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "UPDATE dailyquest set claimed = '1' where quest='" + quest + "';";
        command.ExecuteNonQuery();
      }
      conection.Close();
    }
  }

  public void updateuser(int ID)
  {
    int currentEXP;
    int currentGOLD;
    using (var conection = new SqliteConnection(dbName))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "SELECT * FROM User WHERE ID = '" + ID + "';";
        using (var reader = command.ExecuteReader())
        {
          currentEXP = (int)reader["EXP"];
          currentGOLD = (int)reader["GOLD"];
          Debug.Log(currentGOLD);
          reader.Close();
        }
      }
      currentEXP = currentEXP + getXP;
      currentGOLD = currentGOLD + getGOLD;
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "UPDATE User set EXP = '" + currentEXP + "' WHERE ID ='" + ID + "';";
        command.ExecuteNonQuery();
        command.CommandText = "UPDATE User set GOLD = '" + currentGOLD + "' WHERE ID ='" + ID + "';";
        command.ExecuteNonQuery();
      }
      conection.Close();
    }
  }
}
