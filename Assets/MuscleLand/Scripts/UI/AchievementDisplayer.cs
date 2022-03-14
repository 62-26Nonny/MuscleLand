using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class AchievementDisplayer : MonoBehaviour
{
    private string dbName = "URI=file:DB/server.db";
    public Text squatAchievement;
    public Text jumpAchievement;
    public Text kneeAchievement;

    private void Start() {
        using (var conection = new SqliteConnection(dbName)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM userachievement WHERE userID = '" + Player.userID + "';";
                using (var reader = command.ExecuteReader()){
                    while (reader.Read()){
                        switch((int)reader["arcID"]){
                            case 1:
                                squatAchievement.text = "Squat Lv" + reader["curlvl"].ToString();
                                break;
                            case 2:
                                kneeAchievement.text = "Rising Knee Lv" + reader["curlvl"].ToString();
                                break;
                            case 3:
                                jumpAchievement.text = "Jumping Jack Lv" + reader["curlvl"].ToString();
                                break;
                            default:
                                break;
                        }
                    }
                    reader.Close();
                }
            }
            conection.Close();
        }
    }
}
