using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceCalculator : MonoBehaviour
{
    public float distance;
    public static DistanceCalculator Instance;

    private void Start() {
        Instance = this;
    }

    public void updateDistance(){
        distance = distance_calculate();
    }

    private float distance_calculate(){
        if (LocationTracking.Instance.latitude_old != null && LocationTracking.Instance.longitude_old != null){
            float R = 6371 * Mathf.Pow(10, 3);
            float lat1 = (float)(LocationTracking.Instance.latitude_old * Mathf.PI)/180;
            float lat2 = (LocationTracking.Instance.latitude * Mathf.PI)/180;
            float lon1 = (float)(LocationTracking.Instance.longitude_old * Mathf.PI)/180;
            float lon2 = (LocationTracking.Instance.longitude * Mathf.PI)/180;

            float dif_lat = lat2 - lat1;
            float dif_lon = lon2 - lon1;

            float a = Mathf.Sin(dif_lat/2) * Mathf.Sin(dif_lat/2) + Mathf.Cos(lat1) * Mathf.Cos(lat2) * Mathf.Sin(dif_lon/2) * Mathf.Sin(dif_lon/2);
            float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1-a));
            float d = R * c;

            return d * 10;
        } else {
            return 0f;
        }
    }
}
