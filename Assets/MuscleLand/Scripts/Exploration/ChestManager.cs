using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI.Popup;
public class ChestManager : MonoBehaviour
{
    public Image chest;
    public PopupUI popup;

    private void Update() {
        chest.transform.localPosition = new Vector3(10000 - ProgressBar.Instance.progress.value, -297, 0);
        if (chest.transform.localPosition.x == 0) {
            Button chest_button = chest.GetComponent<Button>();
            chest_button.onClick.AddListener(popup.Show);
        }
    }
    
}
