using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    public Text Gold_Text;
    public Text Exp_Text;
    void Start()
    {
        Gold_Text.text = Player.Gold.ToString();
        Exp_Text.text = Player.Exp.ToString();
    }
}
