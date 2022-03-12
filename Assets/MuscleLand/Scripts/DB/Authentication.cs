using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;

public class Authentication : MonoBehaviour
{
    private string dbName = "URI=file:DB/server.db";
    public InputField loginUsername;
    public InputField loginPassword;
    public InputField registerUsername;
    public InputField registerPassword;
    public InputField registerWeight;
    public Text error;

    public void LoginGuest(){
        using (var conection = new SqliteConnection(dbName)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM User WHERE username = '"+ loginUsername.text + "';";
                using (var reader = command.ExecuteReader()){
                    if (loginPassword.text == reader["password"].ToString()){
                        Debug.Log("Login Sucessful");
                        Player.username = loginUsername.text;
                        Player.userID = reader["ID"].ToString();
                        Player.Gold = (int)reader["GOLD"];
                        Player.EP = (int)reader["EP"];
                        Player.Exp = (int)reader["EXP"] % 100;
                        Player.Level += (int)reader["EXP"] / 100;
                        GetExplorationData();
                        SceneManager.LoadScene("Main Menu");
                    } else {
                        error.gameObject.SetActive(true);
                        Debug.Log("Wrong password");
                    }
                    reader.Close();
                }
            }
            conection.Close();
        }
    }

    public void GetExplorationData(){
        using (var conection = new SqliteConnection(dbName)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM exploration WHERE userID = '"+ Player.userID + "';";
                using (var reader = command.ExecuteReader()){
                    
                    Player.best_progress = (float)reader["bestdistance"];
                    Player.total_progress = (float)reader["totaldistance"];
                    Player.current_progress = (float)reader["currentdistance"] % 10000;
                    Player.total_reward = (int)((float)reader["currentdistance"] / 10000);
                    
                    reader.Close();
                }
            }
            conection.Close();
        }
    }

    public void Register(){
        using (var conection = new SqliteConnection(dbName)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                
                // Create User
                command.CommandText = "INSERT INTO User (username,password,weight) VALUES ('" + registerUsername.text + "','" + registerPassword.text + "','" + registerWeight.text + "');";
                command.ExecuteNonQuery();

                // Get Player ID
                command.CommandText = "SELECT * FROM User WHERE username = '"+ registerUsername.text + "';";
                using (var reader = command.ExecuteReader()){
                    Player.userID = reader["ID"].ToString();

                    reader.Close();
                }

                // Initialized Player data
                command.CommandText = "INSERT INTO wearitem (userID) VALUES ('" + Player.userID + "');";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO exploration (userID) VALUES ('" + Player.userID + "');";
                command.ExecuteNonQuery();

                for (int i = 1; i <= 3; i++){
                    List<string> difficulties = new List<string>{"easy", "medium", "hard"};

                    foreach (string difficulty in difficulties){
                        command.CommandText = "INSERT INTO dungeonstat (userID, dungeonID, difficulty) VALUES ('" + Player.userID + "','" + i + "','" + difficulty + "');";
                        command.ExecuteNonQuery();
                    }
                }
            }
            conection.Close();
        }
    }
}
