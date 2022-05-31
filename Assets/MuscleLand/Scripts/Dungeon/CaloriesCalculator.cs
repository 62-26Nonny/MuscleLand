using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaloriesCalculator : MonoBehaviour
{
    public static CaloriesCalculator Instance;

    private void Start() {
        Instance = this;
    }

    public void Calculate(){
        Player.BurnedCalories += (float)(DungeonValues.Duration / 60f * 8 * 3.5 * Player.weight / 200);
    }
}
