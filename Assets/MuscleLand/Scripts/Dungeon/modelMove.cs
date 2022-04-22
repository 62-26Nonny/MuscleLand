using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class modelMove : MonoBehaviour
{
  public GameObject landmark;
  
  void Update()
  {
    transform.position = new Vector3(landmark.transform.position.x, landmark.transform.position.y, landmark.transform.position.z);
  }
}
