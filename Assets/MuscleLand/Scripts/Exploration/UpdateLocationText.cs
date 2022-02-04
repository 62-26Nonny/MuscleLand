using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLocationText : MonoBehaviour
{
    // public Text latitude;
    // public Text longitude;
    // public Text latitude_old;
    // public Text longitude_old;
    public Text distance;

    private void Update() {
        // latitude.text = "Latitude: " + LocationTracking.latitude.ToString();
        // longitude.text = "Longitude: " + LocationTracking.longitude.ToString();

        // latitude_old.text = "Old Latitude: " + LocationTracking.latitude_old.ToString();
        // longitude_old.text = "Old Longitude: " + LocationTracking.longitude_old.ToString();

        distance.text = "Distance: " + DistanceCalculator.distance.ToString();
    }
}
