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
            var directions = "LR";
            var direction = directions[Random.Range(0, directions.Length)];
            var pos_range_x = 0f;
            if (direction == 'L')
            {
                pos_range_x = Random.Range(minTras, -250);
            }
            else 
            {
                pos_range_x = Random.Range(250, maxTras);
            }
            var position = new Vector3(pos_range_x, -350, 0);
            GameObject gameObject = Instantiate(monsterPrefab[Random.Range(0, monsterPrefab.Length)],
                                    position, Quaternion.identity);
            
            // Set Monster spawn in main canva
            gameObject.transform.SetParent(GameObject.Find("Canvas").transform, true);

            // Wait for Attacking
            yield return new WaitForSeconds(DungeonValues.Interval);

            // If Time out then Escaped
            if (gameObject != null)
            {
                Destroy(gameObject);
                DungeonValues.Combo = 0;
            }
        }
    }
}
