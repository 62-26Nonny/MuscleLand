using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Timer
{
    public class Timer : MonoBehaviour
    {

        [SerializeField] GameObject Popup;
        [SerializeField] GameObject countdown;
        [SerializeField] Text timeText;
        [SerializeField] Text countdownText;

        public float duration, currentTime, countdownTime;

        public static Timer Instance;
       // private float ;

        public GameObject Spawner;
        private Spawner spawner_script;

        void Start()
        {

            switch (GameValues.Difficulty)
            {
                case GameValues.Difficulties.Easy:
                    Debug.Log("Eazy");
                    duration = 30;
                    break;
                case GameValues.Difficulties.Medium:
                    Debug.Log("Medic");
                    duration = 60;
                    break;
                case GameValues.Difficulties.Hard:
                    Debug.Log("Harder");
                    duration = 90;
                    break;
            }


            Instance = this;

            Popup.SetActive(false);
            currentTime = duration;
            timeText.text = currentTime.ToString();
            countdownTime = 3;
            spawner_script = Spawner.GetComponent<Spawner>();
            // StartCoroutine(StartCountDown());
        }

        public IEnumerator StartCountDown()
        {
            while (countdownTime > 0) {
                countdownText.text = countdownTime.ToString();
                yield return new WaitForSeconds(1f);
                countdownTime--;
            }
            countdown.SetActive(false);
            StartCoroutine(TimeIEn());
            StartCoroutine(spawner_script.monsterSpawner());
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
