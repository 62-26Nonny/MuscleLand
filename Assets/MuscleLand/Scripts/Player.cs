using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    public static string username = "";
    public static string userID = "";
    public static Sprite user_profile;
    public static int Level = 0;
    public static int Exp = 0;
    public static int Gold = 0;
    public static int EP = 0;
    public static string[] items;

    private void Start() {
        DontDestroyOnLoad(gameObject);
    }
}
