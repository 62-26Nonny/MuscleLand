using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonValues: MonoBehaviour
{
    public enum Difficulties { Easy, Medium, Hard};
    public static Difficulties Difficulty = Difficulties.Easy;
    public enum Name { Squat, Jump, Knee};
    public static Name Dungeon_name;
    public static int monsterKilled = 0;
    public static int monsterMax = 0;
    public static int Duration = 0;
    public static int Interval = 0;
    public static int Gold_recieved = 0;
    public static int Exp_recieved = 0;

    private void Start() {
        switch(Dungeon_name){
            case Name.Squat:
                monsterMax = 10;
                Interval = 3;
                Duration = monsterMax * Interval;
                break;
            case Name.Jump:
                monsterMax = 20;
                Interval = 2;
                Duration = monsterMax * Interval;
                break;
            case Name.Knee:
                monsterMax = 30;
                Interval = 2;
                Duration = monsterMax * Interval;
                break;
        }

        difficulty_check();
    }

    public void difficulty_check(){
        switch (DungeonValues.Difficulty)
        {
            case DungeonValues.Difficulties.Easy:
                monsterMax *= 1;
                Duration *= 1;
                break;
            case DungeonValues.Difficulties.Medium:
                monsterMax = (int)(monsterMax * 1.5);
                Duration = (int)(Duration * 1.5);
                break;
            case DungeonValues.Difficulties.Hard:
                monsterMax *= 2;
                Duration *= 2;
                break;
        }
    }

    public static void ResetValues(){
        monsterKilled = 0;
    }
}

