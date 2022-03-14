using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class modelMove : MonoBehaviour
{
  [SerializeField] public Text landmark;
  void Start()
    {

  }

    // Update is called once per frame
    void Update()
    {
      transform.position = new Vector3(landmark.transform.position.x, landmark.transform.position.y, landmark.transform.position.z);
  }
}
