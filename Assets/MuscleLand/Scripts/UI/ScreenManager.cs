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
        SFX.Instance.playClickSound();
    }

    public void DungeonPage()
    {
        SceneManager.LoadScene("Select");
        SFX.Instance.playClickSound();
    }

    public void DungeonPlayingPage()
    {
        SceneManager.LoadScene("Playing");
        SFX.Instance.playClickSound();
    }

    public void DungeonDetailgPage()
    {
        SceneManager.LoadScene("Detail");
        SFX.Instance.playClickSound();
    }

    public void SquatPage()
    {
        SceneManager.LoadScene("Detail");
        SFX.Instance.playClickSound();
        DungeonValues.Dungeon_name = DungeonValues.Name.Squat;
    }

    public void JumpPage()
    {
        SceneManager.LoadScene("Detail");
        SFX.Instance.playClickSound();
        DungeonValues.Dungeon_name = DungeonValues.Name.Jump;
    }

    public void KneePage()
    {
        SceneManager.LoadScene("Detail");
        SFX.Instance.playClickSound();
        DungeonValues.Dungeon_name = DungeonValues.Name.Knee;
    }
    public void ExplorationPage()
    {
        SceneManager.LoadScene("MileStone");
        SFX.Instance.playClickSound();
    }
    public void MissionPage()
    {
        SceneManager.LoadScene("Mission");
        SFX.Instance.playClickSound();
    }

    public void AchievementPage()
    {
        SceneManager.LoadScene("Achievement");
        SFX.Instance.playClickSound();
    }

    public void InventoryPage()
    {
        SceneManager.LoadScene("Inventory");
        SFX.Instance.playClickSound();
    }
    public void ShopPage()
    {
        SceneManager.LoadScene("Shop");
        SFX.Instance.playClickSound();
    }
    
    public void ProfilePage(){
        SceneManager.LoadScene("Profile");
        SFX.Instance.playClickSound();
    }
}
