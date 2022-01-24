using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameValues
{
    public enum Difficulties { Easy, Medium, Hard};
    public static Difficulties Difficulty = Difficulties.Easy;

    public static int monsterHealth = 1;
    public static int monsterKill = 0;
    public static int monsterMax = 0;

    public static int Gold = 0;
    public static int Exp = 0;

}

