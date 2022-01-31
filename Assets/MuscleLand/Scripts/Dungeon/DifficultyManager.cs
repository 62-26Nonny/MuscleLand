using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] public GameObject DifficultyToggles;

    private void Start()
    {
        Debug.Log(DungeonValues.Difficulty);
        DifficultyToggles.transform.GetChild((int)DungeonValues.Difficulties.Easy).GetComponent<Toggle>().isOn = true;
    }

    public void SetEasyDifficulty()
    {
        DungeonValues.Difficulty = DungeonValues.Difficulties.Easy;
        Debug.Log("Ez: " + DungeonValues.Difficulty);
    }
    public void SetMediumDifficulty()
    {
        DungeonValues.Difficulty = DungeonValues.Difficulties.Medium;
        Debug.Log("Med: " + DungeonValues.Difficulty);
    }   
    public void SetHardDifficulty()
    {
        DungeonValues.Difficulty = DungeonValues.Difficulties.Hard;
        Debug.Log("Hard: " + DungeonValues.Difficulty);
    }
}

