using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Head_stickman : MonoBehaviour
{
    [SerializeField] private Image head;
    [SerializeField] private RectTransform rt;
    [SerializeField] private GameObject L_shoulder;
    [SerializeField] private GameObject R_shoulder;

    // Update is called once per frame
    void Update()
    {
        var xPos = (L_shoulder.transform.position.x + R_shoulder.transform.position.x)/2;
        var yPos = (L_shoulder.transform.position.y + R_shoulder.transform.position.y)/2 + 50;
        var zPos = (L_shoulder.transform.position.z + R_shoulder.transform.position.z)/2;
        var size = Math.Abs(L_shoulder.transform.position.x - R_shoulder.transform.position.x) / 4;

        head.transform.position = new Vector3(xPos, yPos, -250);
        rt.sizeDelta = new Vector2(70 + size, 70 + size);
    }
}
