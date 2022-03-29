using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class datetimer : MonoBehaviour
{
  public static DateTime currentdate = DateTime.UtcNow;

  private void Start()
  {
    StartCoroutine(nowdate());
  }

  public IEnumerator nowdate()
  {
    while(true)
    {
      yield return new WaitForSeconds(1f);
      currentdate = DateTime.UtcNow;
    }
  }
}
