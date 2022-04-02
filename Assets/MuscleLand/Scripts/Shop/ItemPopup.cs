using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class ItemPopup : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] Text popup_name;
    [SerializeField] Image popup_image;
    [SerializeField] Text popup_des;
    [SerializeField] Text popup_price;
    [SerializeField] Text item_name;
    [SerializeField] Image item_image;
    [SerializeField] Text item_price;
    [SerializeField] GameObject popupBox;
    
    public void Show()
    {
        SFX.Instance.playClickSound();
        popup_name.text = item_name.text;
        popup_image.sprite = item_image.sprite;
        popup_price.text = item_price.text;

        using (var conection = new SqliteConnection(Database.Instance.dbClient))
        {
            conection.Open();
            using (var command = conection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM item WHERE itemID = '"+ transform.name + "';";
                using (var reader = command.ExecuteReader())
                {
                    popupBox.name = reader["itemID"].ToString();
                    popup_des.text = reader["description"].ToString();
                    reader.Close();
                }
            }
            conection.Close();
        }
        canvas.SetActive(true);
    }

    public void Hide()
    {
        SFX.Instance.playClickSound();
        canvas.SetActive(false);
    } 
}

