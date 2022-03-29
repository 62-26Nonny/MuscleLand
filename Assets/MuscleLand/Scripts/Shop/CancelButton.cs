using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour
{
    [SerializeField] GameObject Warning_Text;
    
    public void resetText()
    {
        Warning_Text.SetActive(false);
    }
}
