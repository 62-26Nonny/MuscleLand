using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lr_Testing : MonoBehaviour
{
    [SerializeField] private GameObject[] points;
    [SerializeField] private lr_LineController line;

    public void Start()
    {
        line.SetUpLine(points);
    }

}
