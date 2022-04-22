using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destroyer : MonoBehaviour
{
    [SerializeField] Sprite hit_effect;

    public static GameObject monster;

    public static Destroyer Instance;

    public SFX hit_sound;

    private void Start() {
        Instance = this;
    }

    private void Update() {
        if (GameObject.FindGameObjectsWithTag("Monster").Length > 0) {
            monster = GameObject.FindGameObjectsWithTag("Monster")[0];
        }
    }

    public static void Destruction(){
        if (monster != null) {
            Instance.StartCoroutine(Instance.hit());
        }
    }

    IEnumerator hit(){
        monster.GetComponent<Animator>().enabled = false;
        monster.GetComponent<Image>().sprite = hit_effect;
        hit_sound.playHitSound();
        yield return new WaitForSeconds(0.5f);
        Destroy(monster);
        DungeonValues.monsterKilled++;
        DungeonValues.Combo++;
    }
}


