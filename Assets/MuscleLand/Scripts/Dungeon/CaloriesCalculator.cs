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
        Player.dailyBurnedCalories += (float)((DungeonValues.Duration / 60) * 8 * 3.5 * Player.weight / 200);
        Player.weeklyBurnedCalories += (float)((DungeonValues.Duration / 60) * 8 * 3.5 * Player.weight / 200);
    }
}
