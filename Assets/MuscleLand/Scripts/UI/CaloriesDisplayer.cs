using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaloriesDisplayer : MonoBehaviour
{
    public Text BurnedCalories;

    private void Update() {
        BurnedCalories.text = Player.BurnedCalories.ToString();
    }
}
