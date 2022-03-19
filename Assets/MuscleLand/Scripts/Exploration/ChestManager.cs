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
<<<<<<< HEAD
=======
    public Text stack;
>>>>>>> 60c474e99edb936c3901460c53d9179190044a58
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
        if (isReached & ProgressBar.Instance.total_reward > 1){
            stack.text = "x " + ProgressBar.Instance.total_reward.ToString();
            stack.gameObject.SetActive(true);
        }
    }

    public void resetChest(){
        Button chest_button = chest.GetComponent<Button>();
        chest_button.onClick.RemoveAllListeners();
        isReached = false;
        stack.gameObject.SetActive(false);
    }

    public void resetChest(){
        Button chest_button = chest.GetComponent<Button>();
        chest_button.onClick.RemoveAllListeners();
        isReached = false;
    }
    
}
