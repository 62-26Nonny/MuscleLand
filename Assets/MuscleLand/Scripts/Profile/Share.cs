using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Share : MonoBehaviour
{
    public void Sharing()
    {
        SFX.Instance.playClickSound();
        StartCoroutine(TakeScreenshotAndShare());
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
        ss.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0 );
        ss.Apply();

        string filePath = Path.Combine( Application.temporaryCachePath, "shared img.png" );
        File.WriteAllBytes( filePath, ss.EncodeToPNG() );

        // To avoid memory leaks
        Destroy( ss );

        new NativeShare().AddFile( filePath )
            .SetSubject( "Let's Play Muscle Land!!!" ).SetText( "This game is so funny!" )
            .SetCallback( ( result, shareTarget ) => Debug.Log( "Share result: " + result + ", selected app: " + shareTarget ) )
            .Share();
    }
}
