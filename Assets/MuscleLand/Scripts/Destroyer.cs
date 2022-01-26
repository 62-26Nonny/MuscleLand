using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destroyer : MonoBehaviour
{
    [SerializeField] Sprite hit_effect;

    public static GameObject gameObject;

    public static Destroyer Instance;

    private void Start() {
        Instance = this;
    }

    private void Update() {
        if (GameObject.FindGameObjectsWithTag("Monster").Length > 0) {
            gameObject = GameObject.FindGameObjectsWithTag("Monster")[0];
        }
        // Debug.Log(gameObject);
    }

    public static void Destruction(){
        // Debug.Log("Destroy!!!");
        if (gameObject != null) {
            Instance.StartCoroutine(Instance.hit());
        }
    }

    IEnumerator hit(){
        gameObject.GetComponent<Image>().sprite = hit_effect;
        AudioManager.Instance.SFX();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}


