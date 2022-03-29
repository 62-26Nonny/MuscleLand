using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class arcbutton : MonoBehaviour
{
  public Text XPtext;
  public Text GOLDtext;

  public void rewarding()
  {
    rewardCheck(int.Parse(this.transform.parent.gameObject.name));
  }

  public void rewardCheck(int achievementID)
  {
    StartCoroutine(WebRequest.Instance.GetRequest("/achievement/" + achievementID, (json) => 
    {
      AchievementSerializer[] res = JsonHelper.getJsonArray<AchievementSerializer>(json);
      XPtext.text = res[0].EXP.ToString() + " XP";
      Player.Exp += res[0].EXP;
      GOLDtext.text = res[0].GOLD.ToString() + " GOLD";
      Player.Gold += res[0].GOLD;
      Database.Instance.UpdatePlayer();
      updateclaimed(achievementID);
    }));
  }

  public void updateclaimed(int achievementID)
  {
    int curlvl;
    StartCoroutine(WebRequest.Instance.GetRequest("/userachievement/" + Player.userID + "/" + achievementID, (json) => 
    {
      UserAchievementSerializer[] res = JsonHelper.getJsonArray<UserAchievementSerializer>(json);
      curlvl = res[0].curlvl;
      curlvl = curlvl + 1;
  
      WWWForm form = new WWWForm();
      form.AddField("curlvl", curlvl);
      StartCoroutine(WebRequest.Instance.PostRequest("/userachievement/" + Player.userID + "/" + achievementID, form, (json) => 
      {
        this.gameObject.SetActive(false);
        Achivement.Instance.progressText();
      }));
    }));
  }
}
