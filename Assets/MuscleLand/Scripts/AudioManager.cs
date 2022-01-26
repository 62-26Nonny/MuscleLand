using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] AudioSource bgm_source;
    [SerializeField] AudioSource hit_source;
    [SerializeField] AudioClip hit_sound;
    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SFX(){
        hit_source.PlayOneShot(hit_sound);
    }

    public void play_BGM(){
        bgm_source.Play(0);
    }

    public void stop_BGM(){
        bgm_source.Stop();
    }
}
