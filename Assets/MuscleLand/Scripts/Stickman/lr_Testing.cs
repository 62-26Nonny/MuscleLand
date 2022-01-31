using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lr_Testing : MonoBehaviour
{
    [SerializeField] private Text[] points;
    [SerializeField] private lr_LineController line;

    // Start is called before the first frame update
    public void Start()
    {
        line.SetUpLine(points);
        
    }

}
