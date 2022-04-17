using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
//using Mono.Data.Sqlite;

public class reward : MonoBehaviour
{
    [SerializeField] GameObject test_txt;
    [SerializeField] Add_panel itemManager;

    public int EXP = 10;

    public int lastRecieve = 1;

    public int GOLD = 0;

    public int EP = 0;

    public List<string> rewardList;

    public List<int> amountList;

    public List<string> imgList;

    public void isContain(string name, int amount, string img)
    {
        if (rewardList.Contains(name))
        {
            amountList[rewardList.IndexOf(name)] += amount;
        }
        else
        {
            rewardList.Add(name);
            amountList.Add(amount);
            imgList.Add(img);
        }
    }

    public void mileStone(int index)
    {
        string[] nameList = { "special", "wow" , "test"};
        string[] image = { "games", "games" , "games"};
        isContain(nameList[(int)index/10-1], 1, image[(int)index / 10 - 1]);
    }
    public void showReward()
    {
        rewardList = new List<string>();
        amountList = new List<int>();
        imgList = new List<string>();

        for (int i = lastRecieve;i <= EXP; i++)
        {
            isContain("money", 10, "games");
            if (i%5 == 0)
            {
                isContain("ep", 10, "games");
            }
            /* else if (i%10 != 0)
            {
                isContain("money", 10, "games");
            }
            else
            {
                mileStone(i);
            } */
        }

        itemManager.clear_component();

        for (int j = 0; j < rewardList.Count; j++)
        {
            itemManager.add_component_reward(rewardList[j], amountList[j], imgList[j]);
        }

    }

    public void getReward()
    {
        Text txt = test_txt.transform.GetComponent<Text>();
        string testText = "";
        foreach (int i in amountList)
        {
            testText += i.ToString() + " ";
        }
        txt.text = testText;
    }
    /*
    public void UpdateInventory(int itemID, int amount)
    {
        using (var conection = new SqliteConnection(db_sever))
        {
            conection.Open();
            using (var command = conection.CreateCommand())
            {
                command.CommandText = "INSERT INTO inventory VALUES (" + Player.userID + ", " + itemID + ", " + amount + ");";
                command.ExecuteNonQuery();
            }
            conection.Close();
        }
    }
    public int GetItemID(string itemName)
    {
        int ID = 0;

        using (var conection = new SqliteConnection(db_sever))
        {
            conection.Open();
            using (var command = conection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM item WHERE itemname='" + itemName + "';";
                using (var reader = command.ExecuteReader())
                {


                    ID = int.Parse(reader["itemID"].ToString());


                    reader.Close();
                }
            }
            conection.Close();
        }
        return ID;
    }
    */

}

