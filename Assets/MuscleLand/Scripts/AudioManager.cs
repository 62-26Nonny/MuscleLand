using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Mono.Data.Sqlite;

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
        SFX.Instance.playClickSound();
        settingPopup.gameObject.SetActive(true);
    }

    public void closePopup() {
        settingPopup.gameObject.SetActive(false);
        SFX.Instance.playClickSound();
        using (var conection = new SqliteConnection(Database.Instance.dbClient))
        {
            conection.Open();
            using (var command = conection.CreateCommand()){
                command.CommandText = "UPDATE setting SET SFX = " + Player.effectVolume.ToString() + ", BGM = " + Player.musicVolume.ToString() + ";";
                command.ExecuteNonQuery();
            }
            conection.Close();
        }
    }

    public void UpdateMixerVolume(){
        musicGroup.audioMixer.SetFloat("Music Volume", Mathf.Log10(AudioOptionsManager.musicVolume) * 20);
        effectGroup.audioMixer.SetFloat("Effect Volume", Mathf.Log10(AudioOptionsManager.effectVolume) * 20);
    }


}
