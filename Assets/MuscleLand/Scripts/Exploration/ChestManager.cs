using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChestManager : MonoBehaviour
{
    public Image chest;

    private void Update() {
        chest.transform.position = new Vector3(10000 - ProgressBar.Instance.progress.value, 240, 0);
    }
    
}
