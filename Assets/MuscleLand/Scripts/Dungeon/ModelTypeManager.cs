using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelTypeManager : MonoBehaviour
{
    public GameObject stickman;
    public GameObject avatar;
    public Button stickmanButton;
    public Button avatarButton;

    public void showStickman()
    {
        avatar.SetActive(false);
        stickman.SetActive(true);
        stickmanButton.gameObject.SetActive(false);
        avatarButton.gameObject.SetActive(true);
    }

    public void showAvatar()
    {
        stickman.SetActive(false);
        avatar.SetActive(true);
        avatarButton.gameObject.SetActive(false);
        stickmanButton.gameObject.SetActive(true);
    }
}
