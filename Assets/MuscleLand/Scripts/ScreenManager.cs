using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{

    public BGM Instance;

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
