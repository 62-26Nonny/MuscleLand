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

    private string db_sever = "URI=file:DB/server.db";
    private string db_client = "URI=file:DB/client.db";
    public void EquipThis(){

        UpdateWareItem(GetItemID());

        List<string> appearance_list  = GetAppearanceList();
        if(appearance_list.Count > 0){
            Steve_Image.sprite = Resources.Load<Sprite>(appearance_list[0]);
        } else {
            
        }
        
<<<<<<< HEAD

        
=======
>>>>>>> 60c474e99edb936c3901460c53d9179190044a58
        Item_popup.SetActive(false);

        SceneManager.LoadScene("Inventory");
    } 
    
    public List<string> GetAppearanceList(){
        
        List<string> Equipped_list = EquipList();
        List<string> Appearance_list = new List<string>();

        using (var conection = new SqliteConnection(db_client)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM item ORDER BY itemID ;";
                using (var reader = command.ExecuteReader()){

                        foreach (var item in reader)
                        {
                            if( Equipped_list.Contains(reader["itemID"].ToString()) ){
                                Appearance_list.Add(reader["appearance"].ToString());
                            }

                        }
                    
                    reader.Close();
                }
            }
            conection.Close();
        }
        return Appearance_list;
    }  

     public void UpdateWareItem(string itemID){ 

        using (var conection = new SqliteConnection(db_sever)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                if(string.Equals(GetCurrentWare(), GetItemID())){
<<<<<<< HEAD
                    command.CommandText = "UPDATE wearitem SET itemID = NULL WHERE userID = '" + Player.userID + "';;";
                } else {
                    command.CommandText = "UPDATE wearitem SET itemID = '" + itemID + "' WHERE userID = '" + Player.userID + "';;";
=======
                    command.CommandText = "UPDATE wearitem SET itemID = NULL WHERE userID = '" + Player.userID + "';";
                } else {
                    command.CommandText = "UPDATE wearitem SET itemID = '" + itemID + "' WHERE userID = '" + Player.userID + "';";
>>>>>>> 60c474e99edb936c3901460c53d9179190044a58
                }
                
                command.ExecuteNonQuery();
            }
            conection.Close();
        }

    }

<<<<<<< HEAD


=======
>>>>>>> 60c474e99edb936c3901460c53d9179190044a58
     public string GetItemID(){ 

        string ID = "";

        using (var conection = new SqliteConnection(db_sever)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM item WHERE itemname='" + Item_name.text + "';";
                using (var reader = command.ExecuteReader()){

<<<<<<< HEAD
       
                    ID = reader["itemID"].ToString();
                        
      
=======
                    ID = reader["itemID"].ToString();
                        
>>>>>>> 60c474e99edb936c3901460c53d9179190044a58
                    reader.Close();
                }
            }
            conection.Close();
        }
        return ID;
    }

    public string GetCurrentWare(){ 

        string ID = "";

        using (var conection = new SqliteConnection(db_sever)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM wearitem WHERE userID='" + Player.userID + "';";
                using (var reader = command.ExecuteReader()){

       
                    ID = reader["itemID"].ToString();
                        
      
                    reader.Close();
                }
            }
            conection.Close();
        }
        return ID;
    }

<<<<<<< HEAD



=======
>>>>>>> 60c474e99edb936c3901460c53d9179190044a58
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
