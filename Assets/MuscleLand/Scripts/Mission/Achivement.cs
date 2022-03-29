using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class Achivement : MonoBehaviour
{
  public static Achivement Instance;
  [SerializeField] GameObject achievmentView;
  [SerializeField] GameObject Prefab_achivement;
  public List<Sprite> madelimage;
  public List<GameObject> achivementlist;

  private void Start()
  {
    Instance = this;
    addAchivement();
  }

  public void progressText()
  {
    for (int i = 0; i < achivementlist.Count; i++)
    {
      StartCoroutine(WebRequest.Instance.GetRequest("/achievement/" + achivementlist[i].transform.name, (json) => 
      {
        int progress;
        int goal;
        string achievementName;
        int level;
        int madal;
        int cerrentgoal;
        int dungeonID;
        int achievementID;
        int achievementNumber;

        AchievementSerializer[] res = JsonHelper.getJsonArray<AchievementSerializer>(json);
        goal = res[0].times;
        achievementName = res[0].arcname;
        dungeonID = res[0].dungeonID;
        achievementID = res[0].arcID;
        achievementNumber = res[0].arcID - 1;

        StartCoroutine(WebRequest.Instance.GetRequest("/userachievement/" + Player.userID + "/" + achievementID, (json) => 
        {
          UserAchievementSerializer[] res = JsonHelper.getJsonArray<UserAchievementSerializer>(json);
          level = res[0].curlvl;

          StartCoroutine(WebRequest.Instance.GetRequest("/dungeonstat/sum/" + Player.userID + "/" + dungeonID, (json) => 
          {
            SumSerializer[] res = JsonHelper.getJsonArray<SumSerializer>(json);
            progress = res[0].sum;

            if (level > 3)
            {
              cerrentgoal = goal * 3;
              achivementlist[achievementNumber].transform.Find("RawImage").gameObject.GetComponent<Image>().sprite = madelimage[2];
              achivementlist[achievementNumber].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().maxValue = cerrentgoal;
              achivementlist[achievementNumber].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().value = progress;
              achivementlist[achievementNumber].transform.Find("Archivement").gameObject.GetComponent<Text>().text = achievementName;
              achivementlist[achievementNumber].transform.Find("Archivement info").gameObject.GetComponent<Text>().text = "Play " + achievementName + " " + cerrentgoal.ToString() + " time";
              achivementlist[achievementNumber].transform.Find("progress Text").gameObject.SetActive(false);
              achivementlist[achievementNumber].transform.Find("complete Text").gameObject.SetActive(true);
            }
            else
            {
              madal = level - 1;
              cerrentgoal = goal * level;
              achivementlist[achievementNumber].transform.Find("RawImage").gameObject.GetComponent<Image>().sprite = madelimage[madal];
              achivementlist[achievementNumber].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().maxValue = cerrentgoal;
              achivementlist[achievementNumber].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().value = progress;
              achivementlist[achievementNumber].transform.Find("Archivement").gameObject.GetComponent<Text>().text = achievementName;
              achivementlist[achievementNumber].transform.Find("Archivement info").gameObject.GetComponent<Text>().text = "Play " + achievementName + " " + cerrentgoal.ToString() + " time";
            }

            if (progress < cerrentgoal)
            {
              achivementlist[achievementNumber].transform.Find("progress Text").gameObject.GetComponent<Text>().text = progress.ToString() + "/" + cerrentgoal.ToString();
            }
            else
            {
              if (level <= 3)
              {
                achivementlist[achievementNumber].transform.Find("progress Text").gameObject.GetComponent<Text>().text = cerrentgoal.ToString() + "/" + cerrentgoal.ToString();
                achivementlist[achievementNumber].transform.Find("Button").gameObject.SetActive(true);
              }
            }
          }));
        }));
      }));
    }
  }

  public void addAchivement()
  {
    StartCoroutine(WebRequest.Instance.GetRequest("/achievement", (json) => 
    {
      AchievementSerializer[] res = JsonHelper.getJsonArray<AchievementSerializer>(json);
      foreach (var achievement in res)
      {
        GameObject Clone = Instantiate(Prefab_achivement);

        Clone.SetActive(true);
        Clone.name = achievement.arcID.ToString();
        Clone.transform.SetParent(achievmentView.transform, false);

        Transform CloneTran = Clone.transform;

        achivementlist.Add(Clone);

        progressText();
      }
    }));
  }
}
