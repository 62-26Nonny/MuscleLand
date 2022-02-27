using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class InventoryPopup : MonoBehaviour
{

    
    private string db_sever = "URI=file:DB/server.db";
    private string db_client = "URI=file:DB/client.db";
    [SerializeField] GameObject canvas;
    [SerializeField] Text popup_name;
    [SerializeField] Image popup_image;
    [SerializeField] Text popup_des;

    [SerializeField] Text popup_buttonText;
    [SerializeField] Text item_name;
    [SerializeField] Image item_image;

    
    public void Show(){

        List<string> Equipped_list = EquipList();
        
        popup_name.text = item_name.text;
        popup_image.sprite = item_image.sprite;

        using (var conection = new SqliteConnection(db_client)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM item WHERE itemID = '"+ transform.name + "';";
                using (var reader = command.ExecuteReader()){

                    if(Equipped_list.Contains(reader["itemID"].ToString())){
                        popup_buttonText.text = "Unequip";
                    };
                    popup_des.text = reader["description"].ToString();
      
                    reader.Close();
                }
            }
            conection.Close();
        }

        canvas.SetActive(true);
    }

    // Hide popup
    public void Hide(){
        canvas.SetActive(false);
    } 

    public List<string> EquipList(){

        List<string> Equip_list = new List<string>();
        using (var conection = new SqliteConnection(db_sever)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM wearitem WHERE userID = '" + Player.userID + "';";
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
