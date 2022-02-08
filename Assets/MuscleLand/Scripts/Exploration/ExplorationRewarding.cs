using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationRewarding : MonoBehaviour
{
    public Text Exp;
    public Text EP;

    private void Update() {
        Exp.text = (ProgressBar.Instance.total_reward * 2).ToString();
        EP.text = (ProgressBar.Instance.total_reward * 2).ToString();
    }

    public void Rewarding(){
        Player.EP += ProgressBar.Instance.total_reward * 2;
        Player.Exp += ProgressBar.Instance.total_reward * 2;
        ProgressBar.Instance.ResetProgress();
    }
}
