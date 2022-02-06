using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLocationText : MonoBehaviour
{
    public Text distance;

    private void Start() {
        StartCoroutine(UpdateDistance());
    }

    IEnumerator UpdateDistance(){
        while (true){
            distance.text = "Distance: " + DistanceCalculator.Instance.distance.ToString() + "M";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
