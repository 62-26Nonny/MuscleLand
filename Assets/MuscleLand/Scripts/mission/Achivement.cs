using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class Achivement : MonoBehaviour
{
  private string dbName = "URI=file:DB/server.db";
  public static Achivement Instance;
  [SerializeField] GameObject achievmentView;
  [SerializeField] GameObject Prefab_achivement;
  string userID = Player.userID;
  //string userID = "1";
  public List<Sprite> madelimage;
  public List<GameObject> achivementlist;

  private void Start()
  {
    Instance = this;
    addachivement();
    progresstext();
  }
  void Update()
  {
  }

  public async void progresstext()
  {
    float progress;
    float goal;
    string arc;
    int level;
    int madal;
    float cerrentgoal;
    int dunID;
    int i;
    string arcID;



    for (i = 0; i < achivementlist.Count; i++)
    {
      arcID = achivementlist[i].transform.name;
      using (var conection = new SqliteConnection(dbName))
      {
        conection.Open();
        using (var command = conection.CreateCommand())
        {


          command.CommandText = "SELECT * FROM achievement WHERE arcID = '" + arcID + "';";
          using (var reader = command.ExecuteReader())
          {
            goal = float.Parse(reader["times"].ToString());
            arc = reader["arcname"].ToString();
            dunID = (int)reader["dungeonID"];
          }
          command.CommandText = "SELECT * FROM userachievement WHERE userID == '" + userID + "' AND arcID = '" + arcID + "';";
          using (var reader = command.ExecuteReader())
          {
            level = (int)reader["curlvl"];
          }

          command.CommandText = "SELECT SUM(total) FROM dungeonstat WHERE userID = '" + userID + "' AND dungeonID = '" + dunID + "';";
          using (var reader = command.ExecuteReader())
          {
            progress = float.Parse(reader["SUM(total)"].ToString());
          }

        }
        conection.Close();
      }

      if (level > 3)
      {
        cerrentgoal = goal * 3;
        achivementlist[i].transform.Find("RawImage").gameObject.GetComponent<Image>().sprite = madelimage[2];
        achivementlist[i].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().maxValue = cerrentgoal;
        achivementlist[i].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().value = progress;
        achivementlist[i].transform.Find("Archivement").gameObject.GetComponent<Text>().text = arc;
        achivementlist[i].transform.Find("Archivement info").gameObject.GetComponent<Text>().text = "Play " + arc + " " + cerrentgoal.ToString() + " time";
        achivementlist[i].transform.Find("progress Text").gameObject.GetComponent<Text>().text = cerrentgoal.ToString() + "/" + cerrentgoal.ToString();
        achivementlist[i].transform.Find("complete Text").gameObject.SetActive(true);
      }
      else
      {
        madal = level - 1;
        cerrentgoal = goal * level;
        achivementlist[i].transform.Find("RawImage").gameObject.GetComponent<Image>().sprite = madelimage[madal];
        achivementlist[i].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().maxValue = cerrentgoal;
        achivementlist[i].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().value = progress;
        achivementlist[i].transform.Find("Archivement").gameObject.GetComponent<Text>().text = arc;
        achivementlist[i].transform.Find("Archivement info").gameObject.GetComponent<Text>().text = "Play " + arc + " " + cerrentgoal.ToString() + " time";
      }

      if (progress < cerrentgoal)
      {
        achivementlist[i].transform.Find("progress Text").gameObject.GetComponent<Text>().text = progress.ToString() + "/" + cerrentgoal.ToString();
      }
      else
      {
        if (level <= 3)
        {
          achivementlist[i].transform.Find("progress Text").gameObject.GetComponent<Text>().text = cerrentgoal.ToString() + "/" + cerrentgoal.ToString();
          achivementlist[i].transform.Find("Button").gameObject.SetActive(true);
        }
      }
    }
  }

  public void addachivement()
  {
    using (var conection = new SqliteConnection(dbName))
    {
      conection.Open();
      using (var command = conection.CreateCommand())
      {
        command.CommandText = "SELECT * FROM achievement;";
        using (var reader = command.ExecuteReader())
        {
          foreach (var item in reader)
          {

            GameObject Clone = Instantiate(Prefab_achivement);

            Clone.SetActive(true);
            Clone.name = reader["arcID"].ToString();
            Clone.transform.SetParent(achievmentView.transform, false);

            Transform CloneTran = Clone.transform;

            achivementlist.Add(Clone);
          }
        }
        conection.Close();
      }
    }
  }


}
