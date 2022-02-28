using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class arcbutton : MonoBehaviour
{
  private string dbName = "URI=file:DB/server.db";
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

  public void rewardcheck(int arcID)
  {
    using (var conection = new SqliteConnection(dbName))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "SELECT * FROM achievement WHERE arcID = '" + arcID + "';";
        using (var reader = command.ExecuteReader())
        {
          getXP = (int)reader["EXP"];
          getGOLD = (int)reader["GOLD"];
          reader.Close();
        }
      }
      conection.Close();
    }
    updateclaimed(arcID);
  }

  public void updateclaimed(int arcid)
  {
    int curlvl;
    using (var conection = new SqliteConnection(dbName))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "SELECT * FROM userachievement where userID = '" + userID + "' AND arcid='" + arcid + "';";
        using (var reader = command.ExecuteReader())
        {
          curlvl = (int)reader["curlvl"];
          reader.Close();
        }
        if(curlvl < 3)
        {
          curlvl = curlvl + 1;
        }
        command.CommandText = "UPDATE userachievement set curlvl = '" + curlvl + "' where userID = '" + userID + "' AND arcid='" + arcid + "';";
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
