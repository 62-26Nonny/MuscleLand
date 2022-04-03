using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class WebRequest : MonoBehaviour
{
    public static WebRequest Instance;
    // private string url = "https://muscle-land.herokuapp.com";
    private string url = "http://localhost:3200";

    private void Start() {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator GetRequest(string route, Action<string> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url + route))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = route.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    // Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    callback(webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    public IEnumerator PostRequest(string route, WWWForm form)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url + route, form))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                Debug.Log("Post complete!\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }

    public IEnumerator PostRequest(string route, WWWForm form, Action<string> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url + route, form))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                // Debug.Log("Post complete!\nReceived: " + webRequest.downloadHandler.text);
                callback(webRequest.downloadHandler.text);
            }
        }
    }

}
