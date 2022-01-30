using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM : MonoBehaviour
{
    public static BGM Instance;

    public AudioSource bgm;

    public AudioClip[] audio_list;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
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


}
