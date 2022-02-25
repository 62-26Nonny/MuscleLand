using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
public class InventoryManager : MonoBehaviour
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
    public List<string> GetPathList(){
        
        string userID = GetUserID().ToString();
        List<string> have_list = new List<string>();
        using (var conection = new SqliteConnection(db_sever)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM item WHERE itemID IN (SELECT itemID FROM inventory WHERE userID = '" + userID + "') ORDER BY itemID ;";
                using (var reader = command.ExecuteReader()){
                    
                        foreach(var item in reader)
                        {
                            Debug.Log("ID:" + reader["itemID"] + " Item name:" + reader["itemname"] + " Item price:" + reader["price"]);
                        
                            have_list.Add(reader["itemID"].ToString());
                        }
                        
      
                    reader.Close();
                }
            }
            conection.Close();
        }

        List<string> Path_list = new List<string>();
        
        using (var conection = new SqliteConnection(db_client)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM item ORDER BY itemID ;";
                using (var reader = command.ExecuteReader()){

                        foreach (var item in reader)
                        {
                            if( have_list.Contains(reader["itemID"].ToString()) ){
                                Path_list.Add(reader["pic"].ToString());
                            }

                        }
                        
                        
      
                    reader.Close();
                }
            }
            conection.Close();
        }
        return Path_list;
    }  

    public void AddItem(){
        string userID = GetUserID().ToString();
        List<string> path_list = GetPathList();
         using (var conection = new SqliteConnection(db_sever)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM item WHERE itemID IN (SELECT itemID FROM inventory WHERE userID = '" + userID + "') ORDER BY itemID ;";
                using (var reader = command.ExecuteReader()){
                    
                        
                        int index = 0;

                        foreach(var item in reader)
                        {
                            Debug.Log("ID:" + reader["itemID"] + " Item name:" + reader["itemname"] + " Item price:" + reader["price"]);
                        
                           
                            GameObject box = Instantiate(Prefab_Item);
                            
                            box.SetActive(true);
                            box.name = reader["itemID"].ToString();

                            box.transform.SetParent(list.transform, false);

                            Transform boxtran = box.transform;

                            Transform name = boxtran.Find("name");
                            name.transform.GetComponent<Text>().text = reader["itemname"].ToString();

                            Transform img = boxtran.Find("Image");
                            img.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>(path_list[index]);

                  
                            index++;
                        }
                        
      
                    reader.Close();
                }
            }
            conection.Close();
        }
    }

    public int GetUserID(){ 

        int ID = 0;

        using (var conection = new SqliteConnection(db_sever)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM User WHERE username='" + Player.username + "';";
                using (var reader = command.ExecuteReader()){

       
                    ID = int.Parse(reader["ID"].ToString());
                        
      
                    reader.Close();
                }
            }
            conection.Close();
        }
        return ID;
    }

}
