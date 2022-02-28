using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class datetimer : MonoBehaviour
{
  public static DateTime currentdate = DateTime.UtcNow;
  void Start()
  {
    StartCoroutine(nowdate());
  }

  // Update is called once per frame
  void Update()
  {

  }

  public IEnumerator nowdate()
  {
    while(true)
    {
      yield return new WaitForSeconds(5f);
      currentdate = DateTime.UtcNow;
    }
  }
}
