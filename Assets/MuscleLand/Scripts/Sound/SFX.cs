using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public GameObject SFX_source;
    public AudioSource source;
    [SerializeField] AudioClip sound;
    
    void Start()
    {
        source = SFX_source.GetComponent<AudioSource>();
    }

    public void play(){
        source.PlayOneShot(sound);
    }
}
