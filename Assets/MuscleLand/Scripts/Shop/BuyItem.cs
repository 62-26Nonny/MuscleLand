using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class BuyItem : MonoBehaviour
{
 
    [SerializeField] Text Item_Price;
    [SerializeField] Text Item_name;

    [SerializeField] GameObject Item_popup;

    private string db_sever = "URI=file:DB/server.db";
    public void Buy(){

        string[] textSplit = Item_Price.text.Split();
        int price = int.Parse(textSplit[0]);
        int User_gold = Player.Gold;
    
        if( price < User_gold){

            Player.Gold -= price;
            
            UpdateUser();

            UpdateInventory(GetItemID());
            Item_popup.SetActive(false);
            SceneManager.LoadScene("Shop");

        } else {
            Debug.Log("No gold");
        }

    } 

    public void UpdateUser(){ 

        using (var conection = new SqliteConnection(db_sever)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "UPDATE User SET GOLD = " + Player.Gold + " WHERE username='" + Player.username + "';";
                command.ExecuteNonQuery();
            }
            conection.Close();
        }
    }


     public void UpdateInventory(int itemID){ 

        using (var conection = new SqliteConnection(db_sever)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "INSERT INTO inventory VALUES (" + Player.userID + ", " + itemID + ", " + 1 + ");";
                command.ExecuteNonQuery();
            }
            conection.Close();
        }

    }



     public int GetItemID(){ 

        int ID = 0;

        using (var conection = new SqliteConnection(db_sever)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM item WHERE itemname='" + Item_name.text + "';";
                using (var reader = command.ExecuteReader()){

       
                    ID = int.Parse(reader["itemID"].ToString());
                        
      
                    reader.Close();
                }
            }
            conection.Close();
        }
        return ID;
    }

}
