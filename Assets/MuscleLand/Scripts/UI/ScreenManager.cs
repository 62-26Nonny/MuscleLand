using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{

    public void MainMenuPage()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void DungeonPage()
    {
        SceneManager.LoadScene("Select");
    }

    public void DungeonPlayingPage()
    {
        SceneManager.LoadScene("Playing");
    }

    public void SquatPage()
    {
        SceneManager.LoadScene("Detail");
        DungeonValues.Dungeon_name = DungeonValues.Name.Squat;
    }

    public void JumpPage()
    {
        SceneManager.LoadScene("Detail");
        DungeonValues.Dungeon_name = DungeonValues.Name.Jump;
    }

    public void KneePage()
    {
        SceneManager.LoadScene("Detail");
        DungeonValues.Dungeon_name = DungeonValues.Name.Knee;
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
    
    public void ProfilePage(){
        SceneManager.LoadScene("Profile");
    }
}
