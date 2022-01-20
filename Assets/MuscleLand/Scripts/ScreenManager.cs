using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public GameObject DifficultyToggles;

    private void Start()
    {
        DifficultyToggles.transform.GetChild((int)GameValues.Difficulty).GetComponent<Toggle>().isOn = true;
    }

    #region Difficulty
    public void SetEasyDifficulty(bool isOn)
    {
        Debug.Log("Dif change to Easy");
        if (isOn)
            GameValues.Difficulty = GameValues.Difficulties.Easy;
        Debug.Log("Ez: " + GameValues.Difficulty);
    }
    public void SetMediumDifficulty(bool isOn)
    {
        Debug.Log("Dif change to Medium");
        if (isOn)
            GameValues.Difficulty = GameValues.Difficulties.Medium;
        Debug.Log("Med: " + GameValues.Difficulty);
    }   
    public void SetHardDifficulty(bool isOn)
    {
        Debug.Log("Dif change to Hard");
        if (isOn)
            GameValues.Difficulty = GameValues.Difficulties.Hard;
        Debug.Log("Hard: " + GameValues.Difficulty);
    }
    #endregion

    public void MainMenuPage()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void DungeonPage()
    {
        SceneManager.LoadScene("Select");
    }

    public void DungeonDetailPage()
    {
        SceneManager.LoadScene("Detail");
    }

    public void DungeonPlayPage()
    {
        SceneManager.LoadScene("Playing");
    }
   
    public void ExplorationPage()
    {
        SceneManager.LoadScene("MileStone");
    }
    public void MissionPage()
    {
        SceneManager.LoadScene("Mission");
    }

    public void AchievementPage()
    {
        SceneManager.LoadScene("Achievement");
    }

    public void InventoryPage()
    {
        SceneManager.LoadScene("Inventory");
    }
    public void ShopPage()
    {
        SceneManager.LoadScene("Shop");
    }
}
