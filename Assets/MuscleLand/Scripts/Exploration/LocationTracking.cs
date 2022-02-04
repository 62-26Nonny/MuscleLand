using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class LocationTracking : MonoBehaviour
{
    public static float latitude_old;
    public static float longitude_old;
    public static float latitude;
    public static float longitude;

    private void Start() {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(CheckPermissions());
    }

    private void Update() {
        if (Input.location.lastData.latitude != latitude || Input.location.lastData.longitude != longitude){
            latitude_old = latitude;
            longitude_old = longitude;
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
        }
    }

    private IEnumerator CheckPermissions(){
        while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation)){
            Permission.RequestUserPermission(Permission.FineLocation);
        }

        StartCoroutine(StartLocationService());

        yield break;
    }

    private IEnumerator StartLocationService(){
        if (!Input.location.isEnabledByUser){
            Debug.Log("User has not enable GPS");
            yield break;
        }

        Input.location.Start(5, 1);
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0){
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0){
            Debug.Log("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed){
            Debug.Log("Unable to determine device location");
            yield break;
        }

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        
        yield break;
    }
}
