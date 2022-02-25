using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class Database : MonoBehaviour
{
    private string dbName = "URI=file:DB/server.db";
    public static Database Instance;

    private void Start() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

    }

    public void DungeonRewarding(){ 
        using (var conection = new SqliteConnection(dbName)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "UPDATE User SET GOLD = " + Player.Gold +", EXP = " + Player.Exp + " WHERE username='" + Player.username + "';";
                command.ExecuteNonQuery();
            }
            conection.Close();
        }
    }

    public void ExplorationRewarding(){ 
        using (var conection = new SqliteConnection(dbName)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "UPDATE User SET EP = " + Player.EP +", EXP = " + Player.Exp + " WHERE username='" + Player.username + "';";
                command.ExecuteNonQuery();
            }
            conection.Close();
        }
    }
}
