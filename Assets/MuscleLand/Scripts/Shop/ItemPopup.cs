using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;


public class ItemPopup : MonoBehaviour
{
    private string db_client = "URI=file:DB/client.db";
    [SerializeField] GameObject canvas;
    [SerializeField] Text popup_name;
    [SerializeField] Image popup_image;
    [SerializeField] Text popup_des;
    [SerializeField] Text popup_price;

    [SerializeField] Text item_name;
    [SerializeField] Image item_image;
    [SerializeField] Text item_price;
    
    public void Show(){
        
        popup_name.text = item_name.text;
        popup_image.sprite = item_image.sprite;
        popup_price.text = item_price.text;
        

        using (var conection = new SqliteConnection(db_client)){
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "SELECT * FROM item WHERE itemID = '"+ transform.name + "';";
                using (var reader = command.ExecuteReader()){

                    popup_des.text = reader["description"].ToString();
      
                    reader.Close();
                }
            }
            conection.Close();
        }

        
       // popup_name.text = transform.parent.GetChildCount
        // Transform itemTran = item.transform;

        // Transform name = itemTran.Find("name");
        // Debug.Log("name before replace:" + name.transform.GetComponent<Text>().text);
        // popup_name.text = name.transform.GetComponent<Text>().text;
        // Debug.Log("name after replace:" + name.transform.GetComponent<Text>().text);

        // Transform img = itemTran.Find("Image");
        // popup_image.sprite = img.transform.GetComponent<Image>().sprite;

        canvas.SetActive(true);
    }

    // Hide popup
    public void Hide(){
        canvas.SetActive(false);
    } 
   
}

