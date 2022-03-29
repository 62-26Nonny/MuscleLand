using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class LoadCharacterSkin : MonoBehaviour
{
    private static string db_client = "URI=file:DB/client.db";
    public string current_scene;
    List<string> scence_list = new List<string> {"Inventory", "Main Menu", "Shop", "Profile"};

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(scence_list.Contains(SceneManager.GetActiveScene().name))
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

                using (var conection = new SqliteConnection(db_client))
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

                if(Appearance_list.Count > 0)
                {
                    GameObject.Find("Character").GetComponent<Image>().sprite = Resources.Load<Sprite>(Appearance_list[0]);
                }
            }));
        }
    }
}
