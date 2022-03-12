using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaloriesDisplayer : MonoBehaviour
{
    public Text dailyBurnedCalories;
    public Text weeklyBurnedCalories;

    private void Update() {
        dailyBurnedCalories.text = Player.dailyBurnedCalories.ToString();
        weeklyBurnedCalories.text = Player.weeklyBurnedCalories.ToString();
    }
}
