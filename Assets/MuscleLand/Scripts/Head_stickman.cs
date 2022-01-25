using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Head_stickman : MonoBehaviour
{
    [SerializeField] private Image head;
    [SerializeField] private Text L_shoulder;
    [SerializeField] private Text R_shoulder;

    // Update is called once per frame
    void Update()
    {
        var xPos = (L_shoulder.transform.position.x + R_shoulder.transform.position.x)/2;
        var yPos = (L_shoulder.transform.position.y + R_shoulder.transform.position.y)/2 + 50;

        head.transform.position = new Vector3(xPos, yPos, -250);
    }
}
