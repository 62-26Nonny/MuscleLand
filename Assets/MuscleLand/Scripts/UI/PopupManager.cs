using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Popup;

public class PopupManager : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
        
  }

  public void ShowPopup() 
  {
    PopupUI.Instance.Show();
  }

  public void HidePopup() 
  {
    PopupUI.Instance.Hide();
  }
}
