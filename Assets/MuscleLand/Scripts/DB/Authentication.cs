using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;

public class Authentication : MonoBehaviour
{
    private string dbName = "URI=file:DB/server.db";
    public InputField username;
    public InputField password;

    public void LoginGuest(){
        using (var conection = new SqliteConnection(dbName)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM User WHERE username = '"+ username.text + "';";
                using (var reader = command.ExecuteReader()){
                    if (password.text == reader["password"].ToString()){
                        Debug.Log("Login Sucessful");
                        Player.username = username.text;
                        Player.userID = reader["ID"].ToString();
                        Player.Gold = (int)reader["GOLD"];
                        Player.EP = (int)reader["EP"];
                        Player.Exp = (int)reader["EXP"];
                        SceneManager.LoadScene("Main Menu");
                    } else {
                        Debug.Log("Wrong password");
                    }
                    reader.Close();
                }
            }
            conection.Close();
        }
    }
}
