using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceDisplayer : MonoBehaviour
{
    public Text bestDistance;
    public Text totalDistance;

    public void Update()
    {
        bestDistance.text = ((int)(Player.best_progress / 1000)).ToString();
        totalDistance.text = ((int)(Player.total_progress / 1000)).ToString();
    }
}
