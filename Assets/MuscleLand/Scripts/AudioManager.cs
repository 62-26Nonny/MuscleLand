using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] GameObject settingPopup;
    [SerializeField] AudioMixerGroup musicGroup;
    [SerializeField] AudioMixerGroup effectGroup;

    public void Awake(){
        Instance = this;
    }

    public void openPopup() {
        settingPopup.gameObject.SetActive(true);
    }

    public void closePopup() {
        settingPopup.gameObject.SetActive(false);
    }

    public void UpdateMixerVolume(){
        musicGroup.audioMixer.SetFloat("Music Volume", Mathf.Log10(AudioOptionsManager.musicVolume) * 20);
        effectGroup.audioMixer.SetFloat("Effect Volume", Mathf.Log10(AudioOptionsManager.effectVolume) * 20);
    }


}
