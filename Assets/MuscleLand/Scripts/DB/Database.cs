using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;

public class Database : MonoBehaviour
{
    public static Database Instance;
    List<string> scence_list = new List<string> {"Loading", "Login"} ;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateDungeonData()
    {
        WWWForm form = new WWWForm();
        string url = "/dungeonstat/" + Player.userID + "/" + DungeonValues.Dungeon_ID + "/" + DungeonValues.Difficulty;
        
        if (DungeonValues.Gold_recieved == 0 & DungeonValues.Exp_recieved == 0)
        {
            form = new WWWForm();
            form.AddField("isComplete", "false");
            StartCoroutine(WebRequest.Instance.PostRequest(url, form));
        } 
        else 
        {
            form = new WWWForm();
            form.AddField("isComplete", "true");
            StartCoroutine(WebRequest.Instance.PostRequest(url, form));
        }

        // using (var conection = new SqliteConnection(dbName)){
        //     conection.Open();
        //     using (var command = conection.CreateCommand()){
        //         // Update User Gold and Exp
        //         command.CommandText = "UPDATE User SET GOLD = " + Player.Gold + ", EXP = " + (Player.Exp + (Player.Level - 1) * 100) + " WHERE username='" + Player.username + "';";
        //         command.ExecuteNonQuery();

        //         // Complete or Failed
        //         if (DungeonValues.Gold_recieved == 0 & DungeonValues.Exp_recieved == 0){
        //             // Increase number of failed
        //             command.CommandText = "UPDATE dungeonstat SET fail = fail + 1 WHERE userID = '" + Player.userID + "' AND dungeonID = '" + DungeonValues.Dungeon_ID + "' AND difficulty = '" + DungeonValues.Difficulty + "';";
        //             command.ExecuteNonQuery();
        //         } 
        //         else {
        //             // Increase number of played
        //             command.CommandText = "UPDATE dungeonstat SET daily = daily + 1, weekly = weekly + 1, total = total + 1 WHERE userID='" + Player.userID + "' AND dungeonID = '" + DungeonValues.Dungeon_ID + "' AND difficulty = '" + DungeonValues.Difficulty + "';";
        //             command.ExecuteNonQuery();
        //         }
                
        //     }
        //     conection.Close();
        // }
    }

    public void UpdatePlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("GOLD", Player.Gold);
        form.AddField("EP", Player.EP);
        form.AddField("EXP", Player.Exp + (Player.Level - 1) * 100);
        StartCoroutine(WebRequest.Instance.PostRequest("/user/" + Player.userID, form));
    }

    public void UpdateExplorationData()
    { 
        WWWForm form = new WWWForm();
        form.AddField("currentdistance", (Player.current_progress + (10000 * Player.total_reward)).ToString());
        form.AddField("totaldistance", Player.total_progress.ToString());
        form.AddField("bestdistance", Player.best_progress.ToString());
        StartCoroutine(WebRequest.Instance.PostRequest("/exploration/" + Player.userID, form));

        // using (var conection = new SqliteConnection(dbName))
        // {
        //     conection.Open();
        //     using (var command = conection.CreateCommand()){
        //         command.CommandText = "UPDATE exploration SET currentdistance = " + (Player.current_progress + (10000 * Player.total_reward)) + ", totaldistance = " + Player.total_progress + ", bestdistance = " + Player.best_progress + " WHERE userID='" + Player.userID + "';";
        //         command.ExecuteNonQuery();
        //     }
        //     conection.Close();
        // }
    }
}
