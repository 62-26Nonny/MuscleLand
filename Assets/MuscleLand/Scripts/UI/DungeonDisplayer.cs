using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonDisplayer : MonoBehaviour
{
    public Text ComboCount;
    public Text ComboText;
    public Text Monster;

    private void Update() {
        if (DungeonValues.Combo > 0)
        {
            ComboText.gameObject.SetActive(true);
            ComboCount.gameObject.SetActive(true);
            ComboCount.text = DungeonValues.Combo.ToString();
        }
        else
        {
            ComboText.gameObject.SetActive(false);
            ComboCount.gameObject.SetActive(false);
            ComboCount.text = "??";
        }

        Monster.text = DungeonValues.monsterKilled.ToString() + "/" + DungeonValues.monsterMax.ToString();
    }
}
