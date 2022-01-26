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
        [SerializeField] Image Reward;
        [SerializeField] Sprite[] Reward_rank;
        [SerializeField] Text Header;

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
            AudioManager.Instance.play_BGM();
            GameValues.ResetValues();
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
            AudioManager.Instance.stop_BGM();
            OpenPopup();
            GameValues.Gold += GameValues.monsterKill * 20;
            GameValues.Exp += GameValues.monsterKill * 2;

            if (GameValues.monsterKill == GameValues.monsterMax) {
                Reward.sprite = Reward_rank[0];
            } else if (GameValues.monsterKill >= GameValues.monsterMax * 0.7) {
                Reward.sprite = Reward_rank[1];
            } else if (GameValues.monsterKill >= GameValues.monsterMax * 0.5) {
                Reward.sprite = Reward_rank[2];
            } else {
                Header.text = "Try Better";
                Reward.sprite = Reward_rank[3];
                GameValues.Gold = 0;
                GameValues.Exp = 0;
            }
        }

        void OpenPopup()
        {
            
            Popup.SetActive(true);
        }

   
    }
    
}
