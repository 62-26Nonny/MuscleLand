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
    }

    public void DungeonRewarding(){ 
        using (var conection = new SqliteConnection(dbName)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "UPDATE User SET GOLD = " + Player.Gold + ", EXP = " + (Player.Exp + (Player.Level - 1) * 100) + " WHERE username='" + Player.username + "';";
                command.ExecuteNonQuery();
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
}
