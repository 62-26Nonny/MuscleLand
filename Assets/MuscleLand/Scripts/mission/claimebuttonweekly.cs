using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class claimebuttonweekly : MonoBehaviour
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
    XPtext.text = getXP.ToString() + " XP";
    GOLDtext.text = getGOLD.ToString() + " GOLD";
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
        command.CommandText = "SELECT * FROM weeklyquest WHERE quest = '" + quest + "';";
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
          Player.Exp += (int)reader["EXP"];
          getGOLD = (int)reader["GOLD"];
          Player.Gold += (int)reader["GOLD"];
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
        command.CommandText = "UPDATE weeklyquest set claimed = '1' where quest='" + quest + "';";
        command.ExecuteNonQuery();
      }
      conection.Close();
    }
  }
}
