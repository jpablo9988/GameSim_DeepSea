using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerMethods : MonoBehaviour
{
  public static IEnumerator GeneralTimer(float seconds, Action callbackMethod)
    {
        Debug.Log("Timer start");
        yield return new WaitForSeconds(seconds);
        callbackMethod?.Invoke();
    }
    
}
