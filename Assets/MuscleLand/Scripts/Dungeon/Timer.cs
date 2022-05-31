using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Timer
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] GameObject countdown;
        [SerializeField] Text timeText;
        [SerializeField] Text countdownText;
        public int currentTime;
        public int countdownTime = 3;
        public Spawner spawner_script;
        public DungeonRewarding rewarding_script;
        public MediaPipeManager mediaPipe_script;

        void Start()
        {
            currentTime = DungeonValues.Duration;
            timeText.text = currentTime.ToString();
        }

        public IEnumerator StartCountdown()
        {
            countdownText.gameObject.SetActive(true);
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
            while (currentTime > 0)
            {  
                timeText.text = currentTime.ToString();
                yield return new WaitForSeconds(1f);
                currentTime--;
                if (currentTime <= 5)
                {
                    timeText.color = Color.red;
                }
            }
            rewarding_script.rewarding();

        }
    }
}
