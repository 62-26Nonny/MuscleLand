using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject list;
    [SerializeField] GameObject Prefab_Item;

    private void Start()
    {
        AddItem();
    }

    public void AddItem()
    {   
        // Have list
        List<string> have_list = new List<string>();
        StartCoroutine(WebRequest.Instance.GetRequest("/item/user/" + Player.userID + "/true", (json) => 
        {
            ItemSerializer[] res = JsonHelper.getJsonArray<ItemSerializer>(json);
            foreach (var item in res)
            {
                have_list.Add(item.itemID.ToString());
            }

            // Path list
            List<string> path_list = new List<string>();
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
                            if( have_list.Contains(reader["itemID"].ToString()))
                            {
                                path_list.Add(reader["pic"].ToString());
                            }
                        }
                            
                        reader.Close();
                    }
                }
                conection.Close();
            }

            // Equipped list
            List<string> Equipped_list = new List<string>();
            StartCoroutine(WebRequest.Instance.GetRequest("/wearitem/" + Player.userID, (json) => 
            {
                WearItemSerializer[] res = JsonHelper.getJsonArray<WearItemSerializer>(json);
                foreach (var item in res)
                {
                    Equipped_list.Add(item.itemID.ToString());
                }

                StartCoroutine(WebRequest.Instance.GetRequest("/item/user/" + Player.userID + "/true", (json) => 
                {
                    ItemSerializer[] res = JsonHelper.getJsonArray<ItemSerializer>(json);
                    int index = 0;

                    foreach (var item in res)
                    {
                        GameObject Clone = Instantiate(Prefab_Item);

                        Clone.SetActive(true);
                        Clone.name = item.itemID.ToString();

                        Clone.transform.SetParent(list.transform, false);

                        Transform CloneTran = Clone.transform;

                        Transform Detail = CloneTran.Find("Detail");

                        Transform Equipped = CloneTran.Find("Equipped");

                        Transform name = Detail.Find("Name");
                        name.transform.GetComponent<Text>().text = item.itemname;

                        Transform img = Detail.Find("Image");
                        img.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>(path_list[index]);

                        if(Equipped_list.Contains(item.itemID.ToString()))
                        {
                            Equipped.gameObject.SetActive(true);
                        } 

                        index++;
                    }
                }));
            }));
        }));
    }
}
