using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class LoadCharacterSkin : MonoBehaviour
{
    public string current_scene;
    
    List<string> scence_list = new List<string> {"Inventory", "Main Menu", "Shop", "Profile"};
    private void Awake() {

        DontDestroyOnLoad(gameObject);

    }
    private void Update() {
        
        if( scence_list.Contains(SceneManager.GetActiveScene().name)){
            List<string> Equip_list = Player.GetAppearanceList();
            if(Equip_list.Count > 0){
                GameObject.Find("Character").GetComponent<Image>().sprite = Resources.Load<Sprite>(Equip_list[0]);
            }
            
        }
        
    }

}
