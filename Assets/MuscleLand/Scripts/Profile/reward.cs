using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
//using Mono.Data.Sqlite;

public class reward : MonoBehaviour
{
    [SerializeField] Add_panel PanelManager;

    [SerializeField] GameObject LV_RewardPopup;

    [SerializeField] GameObject rewardButton;

    private int currentLV = 1;

    private int lastRecieve = 1;

    private int GOLD = 0;

    private int EP = 0;

    private List<string> rewardList;

    private List<int> amountList;

    private List<string> imgList;

    void Start(){
        currentLV = Player.Level;
        lastRecieve = Player.lastRewardLV;
        Debug.Log("last recieve " + lastRecieve);
    }

    void Update(){
        if(Player.Level == Player.lastRewardLV){
            rewardButton.SetActive(false);
        } 
        else 
        {
            rewardButton.SetActive(true);
        }
    }


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

    // public void mileStone(int index)
    // {
    //     string[] nameList = { "special", "wow" , "test"};
    //     string[] image = { "games", "games" , "games"};
    //     isContain(nameList[(int)index/10-1], 1, image[(int)index / 10 - 1]);
    // }

    public void showReward()
    {
        //PanelManager.clear_component();
        SFX.Instance.playClickSound();
        LV_RewardPopup.SetActive(true);
        rewardList = new List<string>();
        amountList = new List<int>();
        imgList = new List<string>();

        for (int i = lastRecieve; i < currentLV; i++)
        {
            isContain("Gold", 100, "GameValue/Gold");
            GOLD += 100;
            if (i%5 == 0)
            {
                isContain("EP", 10, "GameValue/EP");
                EP += 10;
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

        for (int j = 0; j < rewardList.Count; j++)
        {
            PanelManager.add_component_reward(rewardList[j], amountList[j], imgList[j]);
        }

    }
    public void recieveReward(){
        SFX.Instance.playClickSound();
        Player.Gold += GOLD;
        Player.EP += EP;
        Player.lastRewardLV = currentLV;
        Database.Instance.UpdatePlayer();
        LV_RewardPopup.SetActive(false);
    }

    // public void getReward()
    // {
    //     Text txt = test_txt.transform.GetComponent<Text>();
    //     string testText = "";
    //     foreach (int i in amountList)
    //     {
    //         testText += i.ToString() + " ";
    //     }
    //     txt.text = testText;
    // }



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

