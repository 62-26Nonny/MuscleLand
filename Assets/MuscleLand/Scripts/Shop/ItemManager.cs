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
        AddItem();
    }
        void Update()
    {
        
    }
    public List<string> Opendata(){
        
        List<string> Path_list = new List<string>();
       
          using (var conection = new SqliteConnection(db_client)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM item ORDER BY itemID;";
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

    public void AddItem(){
        
        List<string> path_list = Opendata();
         using (var conection = new SqliteConnection(db_sever)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM item ORDER BY itemID;";
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

                            Transform price = boxtran.Find("price");
                            price.transform.GetComponent<Text>().text = reader["price"].ToString() + " Gold";
                  
                            index++;
                        }
                        
      
                    reader.Close();
                }
            }
            conection.Close();
        }
    }
}