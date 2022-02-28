using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

[System.Serializable]
public class Player : MonoBehaviour
{
    private static string db_sever = "URI=file:DB/server.db";
    private static string db_client = "URI=file:DB/client.db";
    
    public static string username = "";
    public static string userID = "";
    public static Sprite user_profile;
    public static int Level = 1;
    public static int Exp = 0;
    public static int Gold = 0;
    public static int EP = 0;
    public static int total_reward = 0;
    public static float current_progress = 0f;
    public static string[] items;

    public static List<string>  appearance_list  = GetAppearanceList();

    private void Start() {
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        if (Exp >= 100){
            Level += Exp / 100;
            Exp = Exp % 100;
        }
    }

    public static List<string> GetAppearanceList(){
        
        List<string> Equipped_list = EquipList();
        List<string> Appearance_list = new List<string>();

        using (var conection = new SqliteConnection(db_client)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM item ORDER BY itemID ;";
                using (var reader = command.ExecuteReader()){

                        foreach (var item in reader)
                        {
                            if( Equipped_list.Contains(reader["itemID"].ToString()) ){
                                Appearance_list.Add(reader["appearance"].ToString());
                            }

                        }
                    
                    reader.Close();
                }
            }
            conection.Close();
        }
        return Appearance_list;
    }  

    public static List<string> EquipList(){

        List<string> Equip_list = new List<string>();
        using (var conection = new SqliteConnection(db_sever)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM wearitem WHERE userID = '" + userID + "';";
                using (var reader = command.ExecuteReader()){
                    
                        foreach(var item in reader)
                        {
                            Equip_list.Add(reader["itemID"].ToString());
                        }
                        
      
                    reader.Close();
                }
            }
            conection.Close();
        }
        return Equip_list;
    }

}
