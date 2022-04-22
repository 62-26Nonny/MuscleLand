using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonDisplayer : MonoBehaviour
{
    public Text Combo;
    public Text Monster;

    private void Update() {
        if (DungeonValues.Combo > 0)
        {
            Combo.gameObject.SetActive(true);
            Combo.text = DungeonValues.Combo.ToString();
        }
        else
        {
            Combo.gameObject.SetActive(false);
            Combo.text = "";
        }

        Monster.text = DungeonValues.monsterKilled.ToString() + "/" + DungeonValues.monsterMax.ToString();
    }
}
