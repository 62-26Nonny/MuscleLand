using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] public GameObject DifficultyToggles;

    private void Start()
    {
        Debug.Log(GameValues.Difficulty);
        DifficultyToggles.transform.GetChild((int)GameValues.Difficulties.Easy).GetComponent<Toggle>().isOn = true;
    }

    public void SetEasyDifficulty()
    {
        Debug.Log("Dif change to Easy");
        GameValues.Difficulty = GameValues.Difficulties.Easy;
        Debug.Log("Ez: " + GameValues.Difficulty);
    }
    public void SetMediumDifficulty()
    {
        Debug.Log("Dif change to Medium");
        GameValues.Difficulty = GameValues.Difficulties.Medium;
        Debug.Log("Med: " + GameValues.Difficulty);
    }   
    public void SetHardDifficulty()
    {
        Debug.Log("Dif change to Hard");
        GameValues.Difficulty = GameValues.Difficulties.Hard;
        Debug.Log("Hard: " + GameValues.Difficulty);
    }
}

