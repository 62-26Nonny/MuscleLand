using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class PopupUI : MonoBehaviour
    {
        [SerializeField] GameObject canvas;
        [SerializeField] Button close_button;

        public static PopupUI Instance;

        void Start()
        {
            Instance = this;
        
            close_button.onClick.RemoveAllListeners();
            close_button.onClick.AddListener(Hide);
        }

        // Show popup
        public void Show(){
            canvas.SetActive(true);
            SFX.Instance.playClickSound();
        }

        // Hide popup
        public void Hide(){
            canvas.SetActive(false);
            SFX.Instance.playClickSound();
        } 
    }
}

