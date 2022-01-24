using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterDetail : MonoBehaviour
{
    private int MonsterKilled = 0;
    private int MonsterMax = 0;

    private string detail;

    private void Update()
    {
        MonsterKilled = GameValues.monsterKill;
        MonsterMax = GameValues.monsterMax;
        detail = (MonsterKilled + " / " + MonsterMax);
        this.GetComponent<Text>().text = detail;

    }
}
