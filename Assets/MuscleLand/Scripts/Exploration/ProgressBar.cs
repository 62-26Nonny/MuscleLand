using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider progress;
    public static ProgressBar Instance;

    private void Start() {
        Instance = this;
    }

    public IEnumerator updateProgress(){
        float updateTimed = 1f;
        float inc_distance = DistanceCalculator.Instance.distance / (updateTimed * 100);
        while (updateTimed > 0){
            progress.value += inc_distance;
            yield return new WaitForSeconds(0.01f);
            updateTimed -= 0.01f;
        }
    }
    
}
