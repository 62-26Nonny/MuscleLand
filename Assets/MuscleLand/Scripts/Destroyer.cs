using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public static GameObject gameObject;

    private void Update() {
        gameObject = GameObject.FindGameObjectsWithTag("Monster")[0];
        // Debug.Log(gameObject);
    }

    public static void Destruction(){
        // Debug.Log("Destroy!!!");
        Destroy(gameObject);
    }
}


