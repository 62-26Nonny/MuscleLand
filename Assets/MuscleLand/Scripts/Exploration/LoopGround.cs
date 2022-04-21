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
        float posX = ground.transform.localPosition.x;
        posX -= 1 * distance;
        if (posX <= 0){
            posX = 1200;
        }
        ground.transform.localPosition = new Vector3(posX, -540, -25);
    }
}
