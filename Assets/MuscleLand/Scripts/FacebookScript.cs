// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;
// using Facebook.Unity;

// public class FacebookScript : MonoBehaviour
// {
//     private void Awake()
//     {
//         if (!FB.IsInitialized)
//         {
//             FB.Init(() =>
//             {
//                 if (FB.IsInitialized)
//                     FB.ActivateApp();
//                 else
//                     Debug.LogError("Couldn't initialize");
//             },
//             isGameShown =>
//             {
//                 if (!isGameShown)
//                     Time.timeScale = 0;
//                 else
//                     Time.timeScale = 1;
//             });
//         }
//         else
//             FB.ActivateApp();

//         DontDestroyOnLoad(gameObject);
//     }

//     public void FacebookLogin()
//     {
//         var permissions = new List<string>() { "public_profile", "email", "user_friends" };
//         FB.LogInWithReadPermissions(permissions, LoginCallBack);
//     }

//     private void LoginCallBack(ILoginResult result){
//         if (FB.IsLoggedIn) {
//             GetUserData();
//             SceneManager.LoadScene("Main Menu");
//             SFX.Instance.playClickSound();
//         } else {
//             Debug.Log("User cancelled login");
//         }
//     }

//     private void GetUserData(){
//         FB.API("/me?fields=first_name", HttpMethod.GET, GetUsername);
//         FB.API("/me/picture?type=square&height=1280&width=1280", HttpMethod.GET, GetUserProfile);
//     }

//     private void GetUsername(IResult result){
//         if (result.Error == null)
//         {
//             string name = "" + result.ResultDictionary["first_name"];
//             Player.username = name;
//         }
//         else
//         {
//             Debug.Log(result.Error);
//         }
//     }

//     private void GetUserProfile(IGraphResult result){
//         if (result.Texture != null)
//         {
//             Player.user_profile = Sprite.Create(result.Texture, new Rect(0, 0, 600, 600), new Vector2());
//         }
//         else
//         {
//             Debug.Log(result.Error);
//         }
//     }

//     public void FacebookLogout()
//     {
//         FB.LogOut();
//     }

//     public void FacebookShare()
//     {
//         FB.ShareLink(new System.Uri("https://resocoder.com"), "Check it out!",
//             "Good programming tutorials lol!",
//             new System.Uri("https://resocoder.com/wp-content/uploads/2017/01/logoRound512.png"), ShareCallback);
//     }

//     private void ShareCallback (IShareResult result) {
//         if (result.Cancelled || !string.IsNullOrEmpty(result.Error)) {
//             Debug.Log("ShareLink Error: "+result.Error);
//         } else if (!string.IsNullOrEmpty(result.PostId)) {
//             // Print post identifier of the shared content
//             Debug.Log(result.PostId);
//         } else {
//             // Share succeeded without postID
//             Debug.Log("ShareLink success!");
//         }
//     }
// }
