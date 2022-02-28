using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class Archivement : MonoBehaviour
{
  private string dbName = "URI=file:DB/server.db";
  public List<GameObject> arcbox;
  void Start()
  {
    progresstext();
  }

  // Update is called once per frame
  void Update()
  {

  }

  void progresstext()
  {
    float Svalue;
    float times;
    string arc;
    int level;
    float progress;
    int i;
    int arcID;

    for (i = 0; i < arcbox.Count; i++)
    {
      arcID = i + 1;
      using (var conection = new SqliteConnection(dbName))
      {
        conection.Open();
        using (var command = conection.CreateCommand())
        {
          command.CommandText = "SELECT * FROM achievement WHERE arcID = '" + arcID + "';";
          using (var reader = command.ExecuteReader())
          {
            times = float.Parse(reader["times"].ToString());
            arc = reader["arcname"].ToString();
          }
          command.CommandText = "SELECT * FROM userachievement WHERE userID == 1 AND arcID = '" + arcID + "';";
          using (var reader = command.ExecuteReader())
          {
            level = (int)reader["curlvl"];
          }
        }
        conection.Close();
      }

      progress = times * level;
      Svalue = arcbox[i].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().value;
      arcbox[i].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().maxValue = progress ;
      arcbox[i].transform.Find("Archivement").gameObject.GetComponent<Text>().text = arc;
      arcbox[i].transform.Find("Archivement info").gameObject.GetComponent<Text>().text = "Play " + arc + " " + progress.ToString() + " time";


      if (Svalue < progress)
      {
        arcbox[i].transform.Find("progress Text").gameObject.GetComponent<Text>().text = Svalue.ToString() + "/" + progress.ToString();
      }
      else
      {
        arcbox[i].transform.Find("progress Text").gameObject.GetComponent<Text>().text = times.ToString() + "/" + progress.ToString();
        arcbox[i].transform.Find("Button").gameObject.SetActive(true);
      }
    }

  }
}
