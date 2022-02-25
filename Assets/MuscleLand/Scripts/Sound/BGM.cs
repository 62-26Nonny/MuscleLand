using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM : MonoBehaviour
{
    public static BGM Instance;
    public string current_scene;

    public AudioSource bgm;

    public AudioClip[] audio_list;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            current_scene = SceneManager.GetActiveScene().name;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

    }

    public void stop(){
        bgm.Stop();
    }

    public void play(){
        bgm.Play();
    }

    public void main_menu(){
        bgm.Stop();
        bgm.clip = audio_list[0];
        bgm.Play();
    }
    public void dungeon_play(){
        bgm.Stop();
        bgm.clip = audio_list[1];
        bgm.Play();
    }

    private void Update() {
        if (current_scene != SceneManager.GetActiveScene().name){
            if (current_scene == "Detail" & SceneManager.GetActiveScene().name == "Playing"){
                dungeon_play();
                
            }
            else if (current_scene == "Playing" & (SceneManager.GetActiveScene().name == "Detail" || SceneManager.GetActiveScene().name == "Main Menu")){
                main_menu();
            }
        }
        current_scene = SceneManager.GetActiveScene().name;
    }

}
