using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DetailManager : MonoBehaviour
{
    [SerializeField] Text NameDun;
    [SerializeField] Text DetailDun;
    [SerializeField] Image ImageDun;
    [SerializeField] Text MonsterCount;
    [SerializeField] Text TimePerRound;
    [SerializeField] Sprite[] SpriteList;

    // Start is called before the first frame update
    void Start()
    {
        NameDun.text = DungeonValues.Dungeon_displayname;
        DetailDun.text = DungeonValues.Dungeon_detail;
        switch(DungeonValues.Dungeon_name){
            case DungeonValues.Name.Squat:
                ImageDun.sprite = SpriteList[0];
                break;
            case DungeonValues.Name.Jump:
                ImageDun.sprite = SpriteList[1];
                break;
            case DungeonValues.Name.Knee:
                ImageDun.sprite = SpriteList[2];
                break;
        }
  
    }

    void Update()
    {
        // DungeonValues.difficulty_check();
        SetGameValue();
    }

    public void SetGameValue()
    {
        MonsterCount.text = "X " + DungeonValues.monsterMax.ToString();
        TimePerRound.text = ": " + DungeonValues.Duration.ToString();
    }
}
