using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonValues: MonoBehaviour
{
    public enum Difficulties { Easy, Medium, Hard};
    public static Difficulties Difficulty = Difficulties.Easy;
    public enum Name { Squat, Jump, Knee};
    public static Name Dungeon_name;
    public static string Dungeon_displayname;
    public static string Dungeon_detail;
    public static int monsterKilled = 0;
    public static int monsterMax = 0;
    public static int Duration = 0;
    public static float Interval = 0;
    public static int Gold_recieved = 0;
    public static int Exp_recieved = 0;

    void Start(){
        switch(Dungeon_name){
            case Name.Squat:
                Dungeon_displayname = "Squat";
                Dungeon_detail = "Squat Squat let's to squat";
                monsterMax = 10;
                Interval = 3;
                Duration = (int)(monsterMax * Interval);
                break;
            case Name.Jump:
                Dungeon_displayname = "Jumping Jack";
                Dungeon_detail = "Fight your ways through the enemy by jumping to a position with the legs spread wide and the hands going overhead";
                monsterMax = 20;
                Interval = 1.5f;
                Duration = (int)(monsterMax * Interval);
                break;
            case Name.Knee:
                Dungeon_displayname = "Rising Knee";
                Dungeon_detail = "Fight your ways through the enemy by move your knee up and down repeatly";
                monsterMax = 30;
                Interval = 1;
                Duration = (int)(monsterMax * Interval);
                break;
        }
        
        difficulty_check();
    }

    private void Update() {
        switch(Dungeon_name){
            case Name.Squat:
                Dungeon_displayname = "Squat";
                Dungeon_detail = "Squat Squat let's to squat";
                monsterMax = 10;
                Interval = 3;
                Duration = (int)(monsterMax * Interval);
                break;
            case Name.Jump:
                Dungeon_displayname = "Jumping Jack";
                Dungeon_detail = "Fight your ways through the enemy by jumping to a position with the legs spread wide and the hands going overhead";
                monsterMax = 20;
                Interval = 1.5f;
                Duration = (int)(monsterMax * Interval);
                break;
            case Name.Knee:
                Dungeon_displayname = "Rising Knee";
                Dungeon_detail = "Fight your ways through the enemy by move your knee up and down repeatly";
                monsterMax = 30;
                Interval = 1;
                Duration = (int)(monsterMax * Interval);
                break;
        }
        difficulty_check();
    }

    public static void difficulty_check(){
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
        Duration = 0;
        monsterMax = 0;
    }
}

