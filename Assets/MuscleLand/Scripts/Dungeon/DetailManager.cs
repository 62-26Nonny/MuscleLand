using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class DetailManager : MonoBehaviour
{
  [SerializeField] Text NameDun;
  [SerializeField] Text DetailDun;
  [SerializeField] VideoPlayer Video;
  [SerializeField] Text MonsterCount;
  [SerializeField] Text TimePerRound;
  [SerializeField] VideoClip[] VideoList;
  [SerializeField] Button PlayButton;
  [SerializeField] Button PauseButton;

  // Start is called before the first frame update
  void Start()
  {
    NameDun.text = DungeonValues.Dungeon_displayname;
    DetailDun.text = DungeonValues.Dungeon_detail;
    switch (DungeonValues.Dungeon_name)
    {
      case DungeonValues.Name.Squat:
        Video.clip = VideoList[0];
        break;
      case DungeonValues.Name.Jump:
        Video.clip = VideoList[1];
        break;
      case DungeonValues.Name.Knee:
        Video.clip = VideoList[2];
        break;
    }
    //Video.Prepare();
    Video.Play();
    Video.Pause();
  
  }

  void Update()
  {
    // DungeonValues.difficulty_check();
    SetGameValue();

    if(!Video.isPlaying){
      PlayButton.gameObject.SetActive(true);
    }
  }

  public void SetGameValue()
  {
    MonsterCount.text = "X " + DungeonValues.monsterMax.ToString();
    TimePerRound.text = ": " + DungeonValues.Duration.ToString();

    switch (DungeonValues.Difficulty)
    {    
      case DungeonValues.Difficulties.easy:
          MonsterCount.color = Color.Lerp(Color.green, Color.black, 0.35f);
          TimePerRound.color = Color.Lerp(Color.green, Color.black, 0.35f);
          break;
      case DungeonValues.Difficulties.medium:
          MonsterCount.color = Color.Lerp(Color.yellow, Color.red, 0.5f);
          TimePerRound.color = Color.Lerp(Color.yellow, Color.red, 0.5f);
          break;
      case DungeonValues.Difficulties.hard:
          MonsterCount.color = Color.red;
          TimePerRound.color = Color.red;
          break;
    }

  }

  public void playVideo()
  {
    Video.Play();
    PlayButton.gameObject.SetActive(false);
    PauseButton.gameObject.SetActive(true);
    
  }

  public void pauseVideo()
  {
    Video.Pause();
    PauseButton.gameObject.SetActive(false);
    PlayButton.gameObject.SetActive(true);
    
  }
}
