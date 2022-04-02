using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioOptionsManager : MonoBehaviour
{
    public static float musicVolume { get; private set; }
    public static float effectVolume { get; private set; }
    [SerializeField] private Text musicSilderText;
    [SerializeField] private Text effectSilderText;
    [SerializeField] private Slider musicSilder;
    [SerializeField] private Slider effectSilder;

    public void Start(){

        musicSilder.value = Player.musicVolume;
        effectSilder.value = Player.effectVolume;

        // OnMusicSliderValueChange();
        // OnEffectSliderValueChange();

    }

    public void OnMusicSliderValueChange(){

        musicVolume = musicSilder.value;
        musicSilderText.text = ((int)(musicSilder.value * 100)).ToString();
        Player.musicVolume = musicSilder.value;
        AudioManager.Instance.UpdateMixerVolume();
    }

    public void OnEffectSliderValueChange(){

        effectVolume = effectSilder.value;
        effectSilderText.text = ((int)(effectSilder.value * 100)).ToString();
        Player.effectVolume = effectSilder.value;
        AudioManager.Instance.UpdateMixerVolume();
    }
}
