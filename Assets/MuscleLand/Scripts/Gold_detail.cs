using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold_detail : MonoBehaviour
{
    private int gold = 0;

    void Update()
    {
        gold = GameValues.Gold;
        this.GetComponent<Text>().text = gold.ToString() + " Gold";
    }
}
