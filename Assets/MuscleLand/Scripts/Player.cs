using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

[System.Serializable]
public class Player : MonoBehaviour
{
    public static Player Instance;
    public static string username = "";
    public static string userID = "";
    public static string userpic = "";
    public static Sprite user_profile;
    public static int Level = 1;
    public static int last_LV = 1;
    public static int Exp = 0;
    public static int Gold = 0;
    public static int EP = 0;
    public static int total_reward = 0;
    public static float best_progress = 0f;
    public static float total_progress = 0f;
    public static float current_progress = 0f;
    public static float weight = 0f;
    public static float BurnedCalories = 0f;
    public static string[] items;
    public static float effectVolume = 1f;
    public static float musicVolume = 1f;

    private void Start() {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        if (Exp >= 100){
            Level += Exp / 100;
            Exp = Exp % 100;
        }
    }

    public List<string> GetAppearanceList()
    {
        List<string> Equipped_list = new List<string>();
        List<string> Appearance_list = new List<string>();
        StartCoroutine(WebRequest.Instance.GetRequest("/wearitem/" + Player.userID, (json) => 
        {
            WearItemSerializer[] res = JsonHelper.getJsonArray<WearItemSerializer>(json);
            foreach (var item in res)
            {
                Equipped_list.Add(item.itemID.ToString());
            }

            using (var conection = new SqliteConnection(Database.Instance.dbClient))
            {
                conection.Open();
                using (var command = conection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM item ORDER BY itemID ;";
                    using (var reader = command.ExecuteReader())
                    {
                        foreach (var item in reader)
                        {
                            if(Equipped_list.Contains(reader["itemID"].ToString()))
                            {
                                Appearance_list.Add(reader["appearance"].ToString());
                            }
                        }
                        reader.Close();
                    }
                }
                conection.Close();
            }
        }));

        return Appearance_list;
    }
}
