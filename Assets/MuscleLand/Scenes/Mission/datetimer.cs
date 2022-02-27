using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class datetimer : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    currentdate();
  }

  public void currentdate()
  {
    DateTime curdate = DateTime.UtcNow;
    Debug.Log(curdate);
  }
}
