using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;

public class Authentication : MonoBehaviour
{
    public InputField loginUsername;
    public InputField loginPassword;
    public InputField registerUsername;
    public InputField registerPassword;
    public InputField registerWeight;
    public Text error;

    public void Login(){
        SFX.Instance.playClickSound();
        if (loginUsername.text != "" && loginPassword.text != "")
        {
            StartCoroutine(WebRequest.Instance.GetRequest("/user", (json) => 
            {
                PlayerSerializer[] players = JsonHelper.getJsonArray<PlayerSerializer>(json);
                foreach (var player in players)
                {
                    if (player.username == loginUsername.text && player.password == loginPassword.text)
                    {
                        Debug.Log("Login Sucessful");
                        Player.username = player.username;
                        Player.userID = player.ID.ToString();
                        Player.userpic = player.profilepic;
                        Player.Gold = player.GOLD;
                        Player.EP = player.EP;
                        Player.Exp = player.EXP % 100;
                        Player.Level += player.EXP / 100;
                        Player.lastRewardLV = player.lastRewardLV;
                        Debug.Log("last LV Login " + player.lastRewardLV);
                        GetExplorationData();
                        SceneManager.LoadScene("Main Menu");
                        WWWForm form = new WWWForm();
                        form.AddField("last_active", "online");
                        StartCoroutine(WebRequest.Instance.PostRequest("/user/" + Player.userID, form));
                        return;
                    }
                }
                Debug.Log("Wrong password");
                error.gameObject.SetActive(true);
            }));
        } else {
            Debug.Log("Please enter username and password");
            error.gameObject.SetActive(true);
        }
        
        // using (var conection = new SqliteConnection(dbName)){
        //     conection.Open();
        //     using (var command = conection.CreateCommand()){
        //         command.CommandText = "SELECT * FROM User WHERE username = '"+ loginUsername.text + "';";
        //         using (var reader = command.ExecuteReader()){
        //             if (loginPassword.text == reader["password"].ToString()){
        //                 Debug.Log("Login Sucessful");
        //                 Player.username = loginUsername.text;
        //                 Player.userID = reader["ID"].ToString();
        //                 Player.Gold = (int)reader["GOLD"];
        //                 Player.EP = (int)reader["EP"];
        //                 Player.Exp = (int)reader["EXP"] % 100;
        //                 Player.Level += (int)reader["EXP"] / 100;
        //                 GetExplorationData();
        //                 SceneManager.LoadScene("Main Menu");
        //             } else {
        //                 error.gameObject.SetActive(true);
        //                 Debug.Log("Wrong password");
        //             }
        //             reader.Close();
        //         }
        //     }
        //     conection.Close();
        // }
    }

    public void GetExplorationData(){
        StartCoroutine(WebRequest.Instance.GetRequest("/exploration/" + Player.userID, (json) => 
        {
            ExplorationSerializer[] res = JsonHelper.getJsonArray<ExplorationSerializer>(json);
            Player.best_progress = res[0].bestdistance;
            Player.total_progress = res[0].totaldistance;
            Player.current_progress = res[0].currentdistance % 10000;
            Player.total_reward = (int)(res[0].currentdistance / 10000);
        }));
    
        // using (var conection = new SqliteConnection(dbName)){
        //     conection.Open();
        //     using (var command = conection.CreateCommand()){
        //         command.CommandText = "SELECT * FROM exploration WHERE userID = '"+ Player.userID + "';";
        //         using (var reader = command.ExecuteReader()){
                    
        //             Player.best_progress = (float)reader["bestdistance"];
        //             Player.total_progress = (float)reader["totaldistance"];
        //             Player.current_progress = (float)reader["currentdistance"] % 10000;
        //             Player.total_reward = (int)((float)reader["currentdistance"] / 10000);
                    
        //             reader.Close();
        //         }
        //     }
        //     conection.Close();
        // }
    }

    public void Register()
    {   
        SFX.Instance.playClickSound();
        if (registerUsername.text != "" && registerPassword.text != "")
        {
            WWWForm form = new WWWForm();
            form.AddField("username", registerUsername.text);
            form.AddField("password", registerPassword.text);
            form.AddField("weight", registerWeight.text);

            StartCoroutine(WebRequest.Instance.PostRequest("/user", form, (json) => 
            {
                InsertSerializer player = JsonUtility.FromJson<InsertSerializer>(json);
                Player.userID = player.id.ToString();

                form = new WWWForm();
                form.AddField("userID", Player.userID);

                StartCoroutine(WebRequest.Instance.PostRequest("/wearitem", form));
                StartCoroutine(WebRequest.Instance.PostRequest("/exploration", form));

                for (int i = 1; i <= 3; i++)
                {
                    List<string> difficulties = new List<string>{"easy", "medium", "hard"};

                    foreach (string difficulty in difficulties)
                    {
                        form = new WWWForm();
                        form.AddField("userID", Player.userID);
                        form.AddField("dungeonID", i);
                        form.AddField("difficulty", difficulty);

                        StartCoroutine(WebRequest.Instance.PostRequest("/dungeonstat", form));
                    }
                }

                for (int i = 1; i <= 3; i++)
                {
                    form = new WWWForm();
                    form.AddField("userID", Player.userID);
                    form.AddField("arcID", i);
                    StartCoroutine(WebRequest.Instance.PostRequest("/userachievement", form));
                }

            }));
        }
        else
        {
            Debug.Log("Please enter username and password");
        }

        // using (var conection = new SqliteConnection(dbName)){
        //     conection.Open();
        //     using (var command = conection.CreateCommand()){
                
        //         // Create User
        //         command.CommandText = "INSERT INTO User (username,password,weight) VALUES ('" + registerUsername.text + "','" + registerPassword.text + "','" + registerWeight.text + "');";
        //         command.ExecuteNonQuery();

        //         // Get Player ID
        //         command.CommandText = "SELECT * FROM User WHERE username = '"+ registerUsername.text + "';";
        //         using (var reader = command.ExecuteReader()){
        //             Player.userID = reader["ID"].ToString();

        //             reader.Close();
        //         }

        //         // Initialized Player data
        //         command.CommandText = "INSERT INTO wearitem (userID) VALUES ('" + Player.userID + "');";
        //         command.ExecuteNonQuery();

        //         command.CommandText = "INSERT INTO exploration (userID) VALUES ('" + Player.userID + "');";
        //         command.ExecuteNonQuery();

        //         for (int i = 1; i <= 3; i++){
        //             List<string> difficulties = new List<string>{"easy", "medium", "hard"};

        //             foreach (string difficulty in difficulties){
        //                 command.CommandText = "INSERT INTO dungeonstat (userID, dungeonID, difficulty) VALUES ('" + Player.userID + "','" + i + "','" + difficulty + "');";
        //                 command.ExecuteNonQuery();
        //             }
        //         }
        //     }
            
        //     SceneManager.LoadScene("Main Menu");
        //     conection.Close();
        // }
    }
}
