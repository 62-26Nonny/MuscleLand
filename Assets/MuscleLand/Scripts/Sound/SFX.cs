using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public static SFX Instance;
    public AudioSource source;
    [SerializeField] public AudioClip[] sound;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

    }

    // private void Update() {
    //     if (Input.GetMouseButton(0)) {
    //         playClickSound();
    //     }
    // }

    // void Update()
    // {    
    //     if (Input.GetMouseButton(0))
    //     {
    //         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //         RaycastHit hit;
    //         Physics.Raycast(ray, out hit);
    //         Debug.Log("This hit at " + hit.point );
    //         playClickSound();
    //     }
    // }
    public void playHitSound(){
        source.PlayOneShot(sound[0]);
    }

    public void playClickSound(){
        source.PlayOneShot(sound[1]);
    }
}
