using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonRewarding : MonoBehaviour
{
    public int Gold_max = 200;
    public int Gold_each;
    public int Exp_max = 20;
    public int Exp_each;

    public Sprite[] Reward_rank;
    public Image Reward;
    public Text Header;
    public GameObject Popup;
    public Text Gold_Text;
    public Text Exp_Text;
    public Text Monster_Text;

    private void Start() {
        multiple_reward();
        Gold_each = (int)(Gold_max / DungeonValues.monsterMax);
        Exp_each = (int)(Exp_max / DungeonValues.monsterMax);
    }

    public void multiple_reward(){
        switch (DungeonValues.Difficulty)
        {
            case DungeonValues.Difficulties.Easy:
                Gold_max *= 1;
                Exp_max *= 1;
                break;
            case DungeonValues.Difficulties.Medium:
                Gold_max = (int)(Gold_max * 1.5);
                Exp_max = (int)(Exp_max * 1.5);
                break;
            case DungeonValues.Difficulties.Hard:
                Gold_max *= 2;
                Exp_max *= 2;
                break;
        }
    }

    public void rewarding(){
        // Gold and Exp
        Player.Gold += Gold_each * DungeonValues.monsterKilled;
        Player.Exp += Exp_each * DungeonValues.monsterKilled;
        DungeonValues.Gold_recieved = Gold_each * DungeonValues.monsterKilled;
        DungeonValues.Exp_recieved = Exp_each * DungeonValues.monsterKilled;

        // Rank
        if (DungeonValues.monsterKilled == DungeonValues.monsterMax) {
            Reward.sprite = Reward_rank[0];
        } else if (DungeonValues.monsterKilled >= DungeonValues.monsterMax * 0.7) {
            Reward.sprite = Reward_rank[1];
        } else if (DungeonValues.monsterKilled >= DungeonValues.monsterMax * 0.5) {
            Reward.sprite = Reward_rank[2];
        } else {
            Header.text = "Try Better";
            Reward.sprite = Reward_rank[3];
        }
        showRewardingPopup();
    }

    public void showRewardingPopup(){
        Gold_Text.text = DungeonValues.Gold_recieved.ToString() + " Gold";
        Exp_Text.text = DungeonValues.Exp_recieved.ToString() + " Exp";
        Monster_Text.text = DungeonValues.monsterKilled.ToString() + "/" + DungeonValues.monsterMax.ToString();
        Popup.SetActive(true);
    }
}
