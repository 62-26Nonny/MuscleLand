using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopGround : MonoBehaviour
{
    public static LoopGround Instance;
    public GameObject ground;

    private void Start() {
        Instance = this;
    }

    public void loop(float distance){
        float posX = ground.transform.position.x;
        posX -= 1 * distance;
        if (posX <= 0){
            posX = 1250;
        }
        ground.transform.position = new Vector3(posX, ground.transform.position.y, ground.transform.position.z);
    }
}
