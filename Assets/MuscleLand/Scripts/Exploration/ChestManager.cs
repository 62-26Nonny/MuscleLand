using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI.Popup;
public class ChestManager : MonoBehaviour
{
    public static ChestManager Instance;
    public Image chest;
    public PopupUI popup;
    public bool isReached = false;

    private void Start() {
        Instance = this;
    }

    private void Update() {
        chest.transform.localPosition = new Vector3(10000 - ProgressBar.Instance.progress.value, -297, 0);
        if (chest.transform.localPosition.x == 0 & !isReached) {
            Button chest_button = chest.GetComponent<Button>();
            chest_button.onClick.AddListener(popup.Show);
            chest_button.onClick.AddListener(ExplorationRewarding.Instance.Rewarding);
            isReached = true;
        }
    }

    public void resetChest(){
        Button chest_button = chest.GetComponent<Button>();
        chest_button.onClick.RemoveAllListeners();
        isReached = false;
    }
    
}
