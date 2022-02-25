using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyDisplayer : MonoBehaviour
{
    public Text Gold_Text;
    public Text EP_Text;

    void Update()
    {
        Gold_Text.text = Player.Gold.ToString();
        EP_Text.text = Player.EP.ToString();
    }
}
