using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class ItemManager : MonoBehaviour
{
    [SerializeField] GameObject list;
    [SerializeField] GameObject Prefab_Item;
    

    private void Start()
    {
        AddItem();
    }

    public List<string> GetPathList()
    {
        List<string> Path_list = new List<string>();
    
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
                        Path_list.Add(reader["pic"].ToString());
                    }

                    reader.Close();
                }
            }
            conection.Close();
        }

        return Path_list;
    }  

    public void AddItem()
    {
        List<string> path_list = GetPathList();
        List<string> DontHave = new List<string>();
        StartCoroutine(WebRequest.Instance.GetRequest("/item/user/" + Player.userID + "/false", (json) => 
        {
            ItemSerializer[] res = JsonHelper.getJsonArray<ItemSerializer>(json);
            foreach(var item in res)
            {
                DontHave.Add(item.itemID.ToString());
            }

            StartCoroutine(WebRequest.Instance.GetRequest("/item", (json) => 
            {
                int index = 0;
                int child_lenght = list.transform.childCount;
                ItemSerializer[] res = JsonHelper.getJsonArray<ItemSerializer>(json);

                foreach(var item in res)
                {
                    GameObject Clone = Instantiate(Prefab_Item);

                    Clone.SetActive(true);
                    Clone.name = item.itemID.ToString();

                    Clone.transform.SetParent(list.transform, false);

                    Transform CloneTran = Clone.transform;

                    Transform Detail = CloneTran.Find("Detail");

                    Transform Sold = CloneTran.Find("Sold Out");

                    Transform BuyButton = Detail.Find("Buy Button");

                    Transform name = Detail.Find("Name");
                    name.transform.GetComponent<Text>().text = item.itemname;

                    Transform img = Detail.Find("Image");
                    img.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>(path_list[index]);

                    Transform price = BuyButton.Find("Price");
                    price.transform.GetComponent<Text>().text = item.price.ToString() + " " + item.type;

                    if(DontHave.Contains(item.itemID.ToString()))
                    {
                        Sold.gameObject.SetActive(false);
                    } 
                    else
                    {
                        CloneTran.SetAsLastSibling();
                        Destroy(CloneTran.GetComponent<Button>());
                    }

                    index++;
                }
            }));
        }));

        // using (var conection = new SqliteConnection(db_sever))
        // {
        //     conection.Open();
        //     using (var command = conection.CreateCommand())
        //     {
        //         command.CommandText = "SELECT * FROM item ORDER BY itemID ;";
        //         using (var reader = command.ExecuteReader())
        //         {
        //             List<string> DontHave = NotHaveList();
        //             int index = 0;

        //             int child_lenght = list.transform.childCount;
                    
        //             foreach(var item in  reader)
        //             {
        //                 GameObject Clone = Instantiate(Prefab_Item);

        //                 Clone.SetActive(true);
        //                 Clone.name = reader["itemID"].ToString();

        //                 Clone.transform.SetParent(list.transform, false);

        //                 Transform CloneTran = Clone.transform;

        //                 Transform Detail = CloneTran.Find("Detail");

        //                 Transform Sold = CloneTran.Find("Sold Out");

        //                 Transform BuyButton = Detail.Find("Buy Button");

        //                 Transform name = Detail.Find("Name");
        //                 name.transform.GetComponent<Text>().text = reader["itemname"].ToString();

        //                 Transform img = Detail.Find("Image");
        //                 img.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>(path_list[index]);

        //                 Transform price = BuyButton.Find("Price");
        //                 price.transform.GetComponent<Text>().text = reader["price"].ToString() + " Gold";

        //                 if(DontHave.Contains(reader["itemID"].ToString()) ){
        //                     Sold.gameObject.SetActive(false);
                            
        //                 } else {
        //                     Debug.LogWarning("Sent " + CloneTran.name + " to back");
        //                     CloneTran.SetAsLastSibling();
        //                     Destroy(CloneTran.GetComponent<Button>());
        //                 }

        //                 index++;
        //             }
                        
        //             reader.Close();
        //         }
        //     }
        //     conection.Close();
        // }
    }
}
