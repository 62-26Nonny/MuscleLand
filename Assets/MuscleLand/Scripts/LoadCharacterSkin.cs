using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class LoadCharacterSkin : MonoBehaviour
{
    public string current_scene;
    
    private void Awake() {

        DontDestroyOnLoad(gameObject);

    }

    private void Update() {
        if (current_scene != SceneManager.GetActiveScene().name){
            current_scene = SceneManager.GetActiveScene().name;
            if (current_scene == "Inventory" || current_scene == "Main Menu" || current_scene == "Shop" || current_scene == "Profile"){
                List<string> Equip_list = Player.GetAppearanceList();
                GameObject.Find("Character").GetComponent<Image>().sprite = Resources.Load<Sprite>(Equip_list[0]);

            }
        }
    }

}
