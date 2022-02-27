using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
public class ItemManager : MonoBehaviour
{

    private string db_sever = "URI=file:DB/server.db";
    private string db_client = "URI=file:DB/client.db";

    [SerializeField] GameObject list;
    [SerializeField] GameObject Prefab_Item;

    void Start()
    {
        Debug.LogWarning("Reload shop");
        AddItem();
    }

    void Update()
    {
        
    }
    public List<string> GetPathList(){
        
        
        List<string> Path_list = new List<string>();
        
        using (var conection = new SqliteConnection(db_client)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM item ORDER BY itemID ;";
                using (var reader = command.ExecuteReader()){

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

    public List<string> NotHaveList(){

        List<string> Nothave_list = new List<string>();
        using (var conection = new SqliteConnection(db_sever)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM item WHERE itemID NOT IN (SELECT itemID FROM inventory WHERE userID = '" + Player.userID + "') ORDER BY itemID ;";
                using (var reader = command.ExecuteReader()){
                    
                        foreach(var item in reader)
                        {
                            Nothave_list.Add(reader["itemID"].ToString());
                        }
                        
      
                    reader.Close();
                }
            }
            conection.Close();
        }
        return Nothave_list;
    }

    public void AddItem(){
        List<string> path_list = GetPathList();
         using (var conection = new SqliteConnection(db_sever)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM item ORDER BY itemID ;";
                using (var reader = command.ExecuteReader()){
                    
                        List<string> DontHave = NotHaveList();
                        int index = 0;

                        int child_lenght = list.transform.GetChildCount();
                        
                        foreach(var item in  reader)
                        {

                            GameObject Clone = Instantiate(Prefab_Item);

                            Clone.SetActive(true);
                            Clone.name = reader["itemID"].ToString();

                            Clone.transform.SetParent(list.transform, false);

                            Transform CloneTran = Clone.transform;

                            Transform Detail = CloneTran.Find("Detail");

                            Transform Sold = CloneTran.Find("Sold Out");

                            Transform BuyButton = Detail.Find("Buy Button");

                            Transform name = Detail.Find("Name");
                            name.transform.GetComponent<Text>().text = reader["itemname"].ToString();

                            Transform img = Detail.Find("Image");
                            img.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>(path_list[index]);

                            Transform price = BuyButton.Find("Price");
                            price.transform.GetComponent<Text>().text = reader["price"].ToString() + " Gold";

                            if(DontHave.Contains(reader["itemID"].ToString()) ){
                                Sold.gameObject.SetActive(false);
                                
                            } else {
                                Debug.LogWarning("Sent " + CloneTran.name + " to back");
                                CloneTran.SetSiblingIndex(child_lenght);
                                Clone.GetComponent<Button>().onClick.RemoveAllListeners();
                            }

                            index++;
                        }
                        
      
                    reader.Close();
                }
            }
            conection.Close();
        }
    }

}
