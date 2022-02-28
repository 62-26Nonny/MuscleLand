using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationRewarding : MonoBehaviour
{
    public static ExplorationRewarding Instance;

    public GameObject[] reward_list;
    public Text[] reward_texts;

    private void Start() {
        Instance = this;
    }

    public void Rewarding(){
        reward_texts[0].text = (30 * ProgressBar.Instance.total_reward).ToString() + " Exp";
        reward_texts[1].text = (10 * ProgressBar.Instance.total_reward).ToString() + " EP";
        reward_texts[2].text = (1000 * ProgressBar.Instance.total_reward).ToString() + " G";
        StartCoroutine(RandomReward());
        ProgressBar.Instance.ResetProgress();
        ChestManager.Instance.resetChest();
    }

    IEnumerator RandomReward(){
        System.Random random = new System.Random();
        int index = 0;
        int stack = ProgressBar.Instance.total_reward;
        float time = 1.5f;
        while (time > 0){
            index = index % 3;
            foreach (GameObject reward in reward_list){
                reward.SetActive(false);
            }
            reward_list[index].SetActive(true);
            yield return new WaitForSeconds(0.1f);
            time -= 0.1f;
            index += 1;
        }
        foreach (GameObject reward in reward_list){
            reward.SetActive(false);
        }
        index = random.Next(0, 3);
        reward_list[index].SetActive(true);
        switch (index){
            case 0:
                Player.Exp += 30 * stack;
                break;
            case 1:
                Player.EP += 10 * stack;
                break;
            case 2:
                Player.Gold += 1000 * stack;
                break;
            default:
                break;
        }
    }
}
