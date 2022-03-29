using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class Equipitem : MonoBehaviour
{
    [SerializeField] Text Item_name;
    [SerializeField] Image Steve_Image;
    [SerializeField] GameObject Item_popup;
    private string db_client = "URI=file:DB/client.db";

    public void EquipThis()
    {
        string itemID = "";
        string wearitemID = "";
        StartCoroutine(WebRequest.Instance.GetRequest("/item/" + Item_name.text, (json) => 
        {
            ItemSerializer[] res = JsonHelper.getJsonArray<ItemSerializer>(json);
            itemID = res[0].itemID.ToString();

            StartCoroutine(WebRequest.Instance.GetRequest("/wearitem/" + Player.userID, (json) => 
            {
                WearItemSerializer[] res = JsonHelper.getJsonArray<WearItemSerializer>(json);
                wearitemID = res[0].itemID.ToString();

                WWWForm form = new WWWForm();
        
                if (string.Equals(wearitemID, itemID))
                {
                    form.AddField("itemID", "NULL");
                }
                else
                {
                    form.AddField("itemID", itemID);
                }

                StartCoroutine(WebRequest.Instance.PostRequest("/wearitem/" + Player.userID, form, (json) => 
                {
                    List<string> Equipped_list = new List<string>();
                    StartCoroutine(WebRequest.Instance.GetRequest("/wearitem/" + Player.userID, (json) => 
                    {
                        WearItemSerializer[] res = JsonHelper.getJsonArray<WearItemSerializer>(json);
                        foreach (var item in res)
                        {
                            Equipped_list.Add(item.itemID.ToString());
                        }

                        List<string> Appearance_list = new List<string>();

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
                            Steve_Image.sprite = Resources.Load<Sprite>(Appearance_list[0]);
                        }

                        Item_popup.SetActive(false);
                        SceneManager.LoadScene("Inventory");
                    }));
                }));
            }));
        }));
    }
}
