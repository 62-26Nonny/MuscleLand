using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplayer : MonoBehaviour
{
    public Text username;
    public Image user_profile;
    public Text user_level;
    public Slider EXP_bar;
    public Sprite testimage;

    void Update()
    {   
        username.text = Player.username;
        user_profile.sprite = Resources.Load<Sprite>("Profileimage/"+Player.userpic);
        user_level.text = "Lv." + Player.Level.ToString();
        EXP_bar.value = Player.Exp;
    }
}
