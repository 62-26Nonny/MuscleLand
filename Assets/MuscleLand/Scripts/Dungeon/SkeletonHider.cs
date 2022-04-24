using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHider : MonoBehaviour
{
    public static SkeletonHider Instance;
    public GameObject model;
    private void Start() {
        Instance = this;
    }

    public void hide()
    {
        model.SetActive(false);
    }
}
