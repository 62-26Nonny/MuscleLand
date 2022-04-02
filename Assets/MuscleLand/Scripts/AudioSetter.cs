using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSetter : MonoBehaviour
{
    [SerializeField] AudioMixerGroup musicGroup;
    [SerializeField] AudioMixerGroup effectGroup;

    void Start()
    {
        musicGroup.audioMixer.SetFloat("Music Volume", Mathf.Log10(Player.musicVolume) * 20);
        effectGroup.audioMixer.SetFloat("Effect Volume", Mathf.Log10(Player.effectVolume) * 20);
    }

}
