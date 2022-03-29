using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public static ProgressBar Instance;
    public Slider progress;
    public Text current_progress;
    public bool isFirstBar;
    public float next_bar_value;
    public int total_reward;

    private void Start()
    {
        Instance = this;
        isFirstBar = true;
        total_reward = Player.total_reward;
        
        if (total_reward > 0)
        {
            progress.value = 10000;
            next_bar_value = Player.current_progress;
        }
        else
        {
            progress.value = Player.current_progress;
        }
    }

    private void Update()
    {
        current_progress.text = Mathf.Floor(progress.value / 1000).ToString() + " Km / 10 Km";
    }

    public IEnumerator updateProgress()
    {
        float updateTimed = 1f;
        float inc_distance = DistanceCalculator.Instance.distance / (updateTimed * 100);

        // Set new best distance
        if (DistanceCalculator.Instance.distance > Player.best_progress)
        {
            Player.best_progress = DistanceCalculator.Instance.distance;
        }

        // Increase progress
        while (updateTimed > 0)
        {
            // If reach max distance
            if (progress.value + inc_distance >= 10000 || Player.current_progress + inc_distance >= 10000)
            {
                // Store next bar distance value
                next_bar_value += (progress.value + inc_distance) % 10000;
                Player.current_progress = next_bar_value;
                // If next bar value stack reach max distance
                if (next_bar_value >= 10000)
                {
                    total_reward += (int)Mathf.Floor(next_bar_value / 10000);
                    Player.total_reward += (int)Mathf.Floor(next_bar_value / 10000);
                    next_bar_value = next_bar_value % 10000;
                }
                else if (isFirstBar)
                {
                    // Stack reward following number of reaching max distance
                    total_reward += (int)Mathf.Floor((progress.value + inc_distance) / 10000);
                    Player.total_reward += (int)Mathf.Floor((progress.value + inc_distance) / 10000);
                    isFirstBar = false;
                }
                // Set value to reach max distance
                progress.value = 10000;
            } 
            else 
            {
                progress.value += inc_distance;
                Player.current_progress += inc_distance;
                LoopGround.Instance.loop(inc_distance);
            }
            Player.total_progress += inc_distance;
            yield return new WaitForSeconds(0.01f);
            updateTimed -= 0.01f;
        }
        Database.Instance.UpdateExplorationData();
    }
    
    public void ResetProgress()
    {
        progress.value = next_bar_value;
        Player.current_progress = next_bar_value;
        next_bar_value = 0;
        total_reward = 0;
        Player.total_reward = 0;
        isFirstBar = true;
    }
}
