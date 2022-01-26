using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Timer;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] monsterPrefab;

    [SerializeField] float secondSpawn;

    [SerializeField] float minTras;

    [SerializeField] float maxTras;

    public GameObject Timer;
    private Timer Timer_script;
    
    // Start is called before the first frame update
    void Start()
    {
        switch (GameValues.Difficulty)
        {
            case GameValues.Difficulties.Easy:
               // Debug.Log("Eazy");
                break;
            case GameValues.Difficulties.Medium:
               // Debug.Log("Medic!!!");
                break;
            case GameValues.Difficulties.Hard:
                //Debug.Log("Harder!!!");
                break;
        }
        Timer_script = Timer.GetComponent<Timer>();
        secondSpawn = (Timer_script.duration / 10);

        

        // StartCoroutine(StartCountDown());
        
        //Debug.Log("Duration/10: " + (Timer_script.duration / 10));
    }

    private void Update()
    {
        // Debug.Log("Monster Max: " + ((int)(Timer_script.duration / secondSpawn)));
        GameValues.monsterMax = ((int)(Timer_script.duration / secondSpawn));

        // Debug.Log("currentTime: " + Timer_script.currentTime);
        //Debug.Log("Duration/10: " + (Timer_script.duration / 10));
        // Debug.Log("secondSpawn: " + secondSpawn);
        //Debug.Log("Is Spawn: " + (Timer_script.currentTime >= 3));

    }

    public IEnumerator monsterSpawner()
    {
        
        while (Timer_script.currentTime >= secondSpawn)
        {
            
            var pos_range_x = Random.Range(minTras, maxTras);
            var pos_range_y = Random.Range(GameObject.Find("Canvas").transform.position.y - 300, GameObject.Find("Canvas").transform.position.y + 100); 
            var position = new Vector3(pos_range_x, pos_range_y, GameObject.Find("Canvas").transform.position.z+1);
            GameObject gameObject = Instantiate(monsterPrefab[Random.Range(0, monsterPrefab.Length)],
                                    position, Quaternion.identity);
            Debug.Log("Create monster!");
            void Delete()
            {
                GameValues.monsterKill++;
                Destroy(gameObject);
            }

            gameObject.transform.SetParent(GameObject.Find("Canvas").transform, true);
            gameObject.GetComponent<Button>().onClick.AddListener(Delete);
            

            yield return new WaitForSeconds(secondSpawn);

            Destroy(gameObject);
        }
    }



}
