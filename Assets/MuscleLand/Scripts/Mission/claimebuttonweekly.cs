using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class claimebuttonweekly : MonoBehaviour
{
  private string dbClient = "URI=file:DB/client.db";
  public Text XPtext;
  public Text GOLDtext;
  
  public void rewarding()
  {
    rewardCheck(int.Parse(this.transform.parent.gameObject.name));
  }

  public void rewardCheck(int quest)
  {
    int questID;
    using (var conection = new SqliteConnection(dbClient))
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

    StartCoroutine(WebRequest.Instance.GetRequest("/quest/" + questID, (json) => 
    {
      QuestSerializer[] res = JsonHelper.getJsonArray<QuestSerializer>(json);
      XPtext.text = res[0].EXP.ToString() + " XP";
      GOLDtext.text = res[0].GOLD.ToString() + " GOLD";
      Player.Exp += res[0].EXP;
      Player.Gold += res[0].GOLD;
      Database.Instance.UpdatePlayer();
      updateclaimed(quest);
      this.gameObject.SetActive(false);
      missionprogress.Instance.progresstextweekly();
    }));
  }

  public void updateclaimed(int quest)
  {
    using (var conection = new SqliteConnection(dbClient))
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
