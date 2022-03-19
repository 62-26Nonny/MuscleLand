using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;

public class Database : MonoBehaviour
{
    private string dbName = "URI=file:DB/server.db";
    public static Database Instance;
    List<string> scence_list = new List<string> {"Loading", "Login"} ;

    private void Start() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        if (scence_list.Contains(SceneManager.GetActiveScene().name))
        StartCoroutine(UpdatePlayer());
        StartCoroutine(UpdateExplorationData());
    }

    public void DungeonRewarding(){ 
        using (var conection = new SqliteConnection(dbName)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                // Update User Gold and Exp
                command.ExecuteNonQuery();
                // Complete or Failed
                if (DungeonValues.Gold_recieved == 0 & DungeonValues.Exp_recieved == 0){
                    // Increase number of failed
                    command.CommandText = "UPDATE dungeonstat SET fail = fail + 1 WHERE userID = '" + Player.userID + "' AND dungeonID = '" + DungeonValues.Dungeon_ID + "' AND difficulty = '" + DungeonValues.Difficulty + "';";
                    command.ExecuteNonQuery();
                } 
                else {
                    // Increase number of played
                    command.CommandText = "UPDATE dungeonstat SET daily = daily + 1, weekly = weekly + 1, total = total + 1 WHERE userID='" + Player.userID + "' AND dungeonID = '" + DungeonValues.Dungeon_ID + "' AND difficulty = '" + DungeonValues.Difficulty + "';";
                    command.ExecuteNonQuery();
                }
                
            }
            conection.Close();
        }
    }

    IEnumerator UpdatePlayer(){ 
        while (true){
            using (var conection = new SqliteConnection(dbName)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "UPDATE User SET GOLD = " + Player.Gold + ", EP = " + Player.EP + ", EXP = " + (Player.Exp + (Player.Level - 1) * 100) + " WHERE username='" + Player.username + "';";
                command.ExecuteNonQuery();
            }
            conection.Close();
        }
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator UpdateExplorationData(){ 
        while (true){
            using (var conection = new SqliteConnection(dbName)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "UPDATE exploration SET currentdistance = " + (Player.current_progress + (10000 * Player.total_reward)) + ", totaldistance = " + Player.total_progress + ", bestdistance = " + Player.best_progress + " WHERE userID='" + Player.userID + "';";
                command.ExecuteNonQuery();
            }
            conection.Close();
        }
            yield return new WaitForSeconds(1f);
        }
    }
}
