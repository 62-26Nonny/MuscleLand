using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class FacebookScript : MonoBehaviour
{
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                    FB.ActivateApp();
                else
                    Debug.LogError("Couldn't initialize");
            },
            isGameShown =>
            {
                if (!isGameShown)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;
            });
        }
        else
            FB.ActivateApp();

        DontDestroyOnLoad(gameObject);
    }

    public void FacebookLogin()
    {
        var permissions = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(permissions);
    }

    public void FacebookLogout()
    {
        FB.LogOut();
    }

     public void FacebookShare()
    {
        FB.ShareLink(new System.Uri("https://resocoder.com"), "Check it out!",
            "Good programming tutorials lol!",
            new System.Uri("https://resocoder.com/wp-content/uploads/2017/01/logoRound512.png"));
    }
}
