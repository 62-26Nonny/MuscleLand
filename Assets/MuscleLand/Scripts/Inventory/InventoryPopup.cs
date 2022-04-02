using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class InventoryPopup : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] Text popup_name;
    [SerializeField] Image popup_image;
    [SerializeField] Text popup_des;
    [SerializeField] Text popup_buttonText;
    [SerializeField] Text item_name;
    [SerializeField] Image item_image;

    public void Show()
    {
        List<string> Equipped_list = new List<string>();
        StartCoroutine(WebRequest.Instance.GetRequest("/wearitem/" + Player.userID, (json) => 
        {
            WearItemSerializer[] res = JsonHelper.getJsonArray<WearItemSerializer>(json);
            foreach (var item in res)
            {
                Equipped_list.Add(item.itemID.ToString());
            }

            popup_name.text = item_name.text;
            popup_image.sprite = item_image.sprite;

            using (var conection = new SqliteConnection(Database.Instance.dbClient))
            {
                conection.Open();
                using (var command = conection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM item WHERE itemID = '"+ transform.name + "';";
                    using (var reader = command.ExecuteReader())
                    {
                        if(Equipped_list.Contains(reader["itemID"].ToString()))
                        {
                            popup_buttonText.text = "Unequip";
                        };

                        popup_des.text = reader["description"].ToString();
                        reader.Close();
                    }
                }
                conection.Close();
            }

            canvas.SetActive(true);
        }));
    }

    public void Hide()
    {
        canvas.SetActive(false);
    } 
}
