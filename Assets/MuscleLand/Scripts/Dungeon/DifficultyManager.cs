using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] public GameObject DifficultyToggles;

    private void Start()
    {
        DifficultyToggles.transform.GetChild((int)DungeonValues.Difficulties.easy).GetComponent<Toggle>().isOn = true;
    }

    public void SetEasyDifficulty()
    {
        SFX.Instance.playClickSound();
        DungeonValues.Difficulty = DungeonValues.Difficulties.easy;
        DungeonValues.difficulty_check();
    }
    public void SetMediumDifficulty()
    {
        SFX.Instance.playClickSound();
        DungeonValues.Difficulty = DungeonValues.Difficulties.medium;
        DungeonValues.difficulty_check();
    }   
    public void SetHardDifficulty()
    {
        SFX.Instance.playClickSound();
        DungeonValues.Difficulty = DungeonValues.Difficulties.hard;
        DungeonValues.difficulty_check();
    }
}

