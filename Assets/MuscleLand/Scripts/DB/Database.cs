using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;

public class Database : MonoBehaviour
{
    public static Database Instance;
    List<string> scence_list = new List<string> {"Loading", "Login"};
    public string dbClient = "URI=file:DB/client.db";

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
    }

    void OnApplicationQuit()
    {
        WWWForm form = new WWWForm();
        form.AddField("last_active", DateTime.UtcNow.ToString());
        StartCoroutine(WebRequest.Instance.PostRequest("/user/" + Player.userID, form));
    }
}
