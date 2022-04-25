using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;

public class Database : MonoBehaviour
{
    public static Database Instance;
    List<string> scence_list = new List<string> {"Loading", "Login"};
    public string dbClient;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            dbClient = Application.persistentDataPath + "/client.db";
            if(!File.Exists(dbClient))
            {
                Debug.LogWarning("File \"" + dbClient + "\" does not exist. Attempting to create from \"" +
                                Application.dataPath + "!/assets/client.db");
                // if it doesn't ->
                // open StreamingAssets directory and load the db -> 
                WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/client.db");
                while(!loadDB.isDone) {}
                // then save to Application.persistentDataPath
                File.WriteAllBytes(dbClient, loadDB.bytes);
            }
            //open db connection
            dbClient = "URI=file:" + dbClient;
            DontDestroyOnLoad(gameObject);
            InitThaiCalendarCrashFix();
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
        if (Player.Exp + (Player.Level - 1) * 100 > 30000)
        {
            Player.Exp = 100;
            Player.Level = 300;
        }
        WWWForm form = new WWWForm();
        form.AddField("GOLD", Player.Gold);
        form.AddField("EP", Player.EP);
        form.AddField("EXP", Player.Exp + (Player.Level - 1) * 100);
        form.AddField("last_LV", Player.last_LV);
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

    private void OnApplicationQuit()
    {
        if (Player.userID != "")
        {
            Debug.LogWarning("quit");
            WWWForm form = new WWWForm();
            form.AddField("last_active", DateTime.UtcNow.ToString());
            StartCoroutine(WebRequest.Instance.PostRequest("/user/" + Player.userID, form));
        }
    }

    private void OnApplicationFocus(bool focusStatus) {
        if (Player.userID != "" && focusStatus)
        {
            Debug.LogWarning("focus");
            WWWForm form = new WWWForm();
            form.AddField("last_active", "online");
            StartCoroutine(WebRequest.Instance.PostRequest("/user/" + Player.userID, form));
        }
        else if (Player.userID != "" && !focusStatus)
        {
            OnApplicationQuit();
        }
    }

    private static void InitThaiCalendarCrashFix()
    {
        if (Application.systemLanguage == SystemLanguage.Thai)
        {
            Debug.LogWarning("Thai");
            new System.Globalization.ThaiBuddhistCalendar();
        }
    }
}
