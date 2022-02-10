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
        DungeonValues.difficulty_check();
        Debug.Log("Ez: " + DungeonValues.Difficulty);
    }
    public void SetMediumDifficulty()
    {
        DungeonValues.Difficulty = DungeonValues.Difficulties.Medium;
        DungeonValues.difficulty_check();
        Debug.Log("Med: " + DungeonValues.Difficulty);
    }   
    public void SetHardDifficulty()
    {
        DungeonValues.Difficulty = DungeonValues.Difficulties.Hard;
        DungeonValues.difficulty_check();
        Debug.Log("Hard: " + DungeonValues.Difficulty);
    }
}

