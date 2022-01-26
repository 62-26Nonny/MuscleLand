using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XP_detail : MonoBehaviour
{
    private int exp = 0;

    void Update()
    {
        exp = GameValues.Exp;
        this.GetComponent<Text>().text = exp.ToString() + " Exp";
    }
}
