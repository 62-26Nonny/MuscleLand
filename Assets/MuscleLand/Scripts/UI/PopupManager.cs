using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Popup;

public class PopupManager : MonoBehaviour
{
  public void ShowPopup() 
  {
    PopupUI.Instance.Show();
  }

  public void HidePopup() 
  {
    PopupUI.Instance.Hide();
  }
}
