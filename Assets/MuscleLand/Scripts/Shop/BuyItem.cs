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
    [SerializeField] GameObject Warning_Text;
    [SerializeField] GameObject Item_popup;
    [SerializeField] GameObject itemID;

    public void Buy()
    {
        string[] textSplit = Item_Price.text.Split();
        int price = int.Parse(textSplit[0]);
        string type = textSplit[1];

        if (type == "Gold")
        {
            if(price < Player.Gold)
            {
                Player.Gold -= price;
                Database.Instance.UpdatePlayer();
                UpdateInventory();
                Item_popup.SetActive(false);
                SceneManager.LoadScene("Shop");
            } 
            else
            {
                Debug.Log("No gold");
                Warning_Text.SetActive(true);
            }
        }
        else
        {
            if(price < Player.EP)
            {
                Player.EP -= price;
                Database.Instance.UpdatePlayer();
                UpdateInventory();
                Item_popup.SetActive(false);
                SceneManager.LoadScene("Shop");
            } 
            else
            {
                Debug.Log("No gold");
                Warning_Text.SetActive(true);
            }
        }
    
        
    }

    public void UpdateInventory()
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", Player.userID);
        form.AddField("itemID", itemID.name);
        form.AddField("amount", 1);
        StartCoroutine(WebRequest.Instance.PostRequest("/inventory", form));

        // using (var conection = new SqliteConnection(db_sever))
        // {
        //     conection.Open();
        //     using (var command = conection.CreateCommand())
        //     {
        //         command.CommandText = "INSERT INTO inventory VALUES (" + Player.userID + ", " + itemID + ", " + 1 + ");";
        //         command.ExecuteNonQuery();
        //     }
        //     conection.Close();
        // }
    }

    public string GetItemID()
    { 
        string ID = "";
        StartCoroutine(WebRequest.Instance.GetRequest("/item/" + Item_name.text, (json) => 
        {
            ItemSerializer[] res = JsonHelper.getJsonArray<ItemSerializer>(json);
            ID = res[0].itemID.ToString();
        }));

        // using (var conection = new SqliteConnection(db_sever))
        // {
        //     conection.Open();
        //     using (var command = conection.CreateCommand())
        //     {
        //         command.CommandText = "SELECT * FROM item WHERE itemname='" + Item_name.text + "';";
        //         using (var reader = command.ExecuteReader())
        //         {
        //             ID = reader["itemID"].ToString();
        //             reader.Close();
        //         }
        //     }
        //     conection.Close();
        // }

        return ID;
    }
}
