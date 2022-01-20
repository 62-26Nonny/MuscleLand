using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class Popup
    {
        
    }

    public class PopupUI : MonoBehaviour
    {
        [SerializeField] GameObject canvas;
        [SerializeField] Button claimButton;

        Popup popup = new Popup();

        public static PopupUI Instance;

        // Start is called before the first frame update
        void Start()
        {
            Instance = this;

            claimButton.onClick.RemoveAllListeners();
            claimButton.onClick.AddListener(Hide);
        }

        // Update is called once per frame
        void Update()
        {

        }

        // Show popup
        public void Show(){
            canvas.SetActive(true);
        }

        // Hide popup
        public void Hide(){
            canvas.SetActive(false);
        } 
    }
}

