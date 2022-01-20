using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Timer
{
    public class Timer : MonoBehaviour
    {

        [SerializeField] GameObject Popup;

        [SerializeField] Text timeText;

        public float duration, currentTime;

        public static Timer Instance;
       // private float ;


        void Start()
        {

            switch (GameValues.Difficulty)
            {
                case GameValues.Difficulties.Easy:
                    Debug.Log("Eazy");
                    duration = 10;
                    break;
                case GameValues.Difficulties.Medium:
                    Debug.Log("Medic");
                    duration = 20;
                    break;
                case GameValues.Difficulties.Hard:
                    Debug.Log("Harder");
                    duration = 30;
                    break;
            }


            Instance = this;

            Popup.SetActive(false);
            currentTime = duration;
            timeText.text = currentTime.ToString();
            StartCoroutine(TimeIEn());
        }

        IEnumerator TimeIEn()
        {
            while (currentTime >= 0)
            {
                timeText.text = currentTime.ToString();
                yield return new WaitForSeconds(1f);
                currentTime--;
                if (currentTime <= 5)
                {
                    timeText.color = Color.red;
                }
            }

            OpenPopup();
        }

        void OpenPopup()
        {
            Popup.SetActive(true);
        }
    }
    
}
