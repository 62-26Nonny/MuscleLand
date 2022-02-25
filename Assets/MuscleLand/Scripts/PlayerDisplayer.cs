using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplayer : MonoBehaviour
{
    public Text username;
    public Image user_profile;

    void Update()
    {
        username.text = Player.username;
        user_profile.sprite = Player.user_profile;
    }
}
