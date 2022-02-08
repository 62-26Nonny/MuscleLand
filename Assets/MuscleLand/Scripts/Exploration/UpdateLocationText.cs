using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLocationText : MonoBehaviour
{
    public Text distance;
    public Text latitude;
    public Text longitude;
    public Text latitude_old;
    public Text longitude_old;

    private void Start() {
        StartCoroutine(UpdateDistance());
    }

    IEnumerator UpdateDistance(){
        while (true){
            distance.text = "Distance: " + DistanceCalculator.Instance.distance.ToString() + "M";
            latitude.text = "Latitude: " + LocationTracking.Instance.latitude.ToString();
            longitude.text = "Longitude: " + LocationTracking.Instance.longitude.ToString();
            latitude_old.text = "Latitude: " + LocationTracking.Instance.latitude_old.ToString();
            longitude_old.text = "Longitude: " + LocationTracking.Instance.longitude_old.ToString();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
