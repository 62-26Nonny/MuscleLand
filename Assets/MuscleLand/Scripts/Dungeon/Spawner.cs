using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Timer;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] monsterPrefab;
    [SerializeField] float minTras;
    [SerializeField] float maxTras;
    public Timer Timer_script;
    
    public IEnumerator monsterSpawner()
    {
        
        while (Timer_script.currentTime >= DungeonValues.Interval)
        {
            
            var pos_range_x = Random.Range(minTras, maxTras);
            var pos_range_y = Random.Range(GameObject.Find("Canvas").transform.position.y - 300, GameObject.Find("Canvas").transform.position.y + 100); 
            var position = new Vector3(pos_range_x, pos_range_y, GameObject.Find("Canvas").transform.position.z+1);
            GameObject gameObject = Instantiate(monsterPrefab[Random.Range(0, monsterPrefab.Length)],
                                    position, Quaternion.identity);
            
            // Set Monster spawn in main canva
            gameObject.transform.SetParent(GameObject.Find("Canvas").transform, true);

            // Wait for Attacking
            yield return new WaitForSeconds(DungeonValues.Interval);

            // If Time out then Escaped
            Destroy(gameObject);
        }
    }
}
