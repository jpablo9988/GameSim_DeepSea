using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerMethods : MonoBehaviour
{
    public static IEnumerator GeneralTimer(float seconds, Action callbackMethod)
    {
        float remainingSeconds = seconds;
        yield return new WaitForSeconds(seconds);
        callbackMethod?.Invoke();
    }
    public static IEnumerator WaitForNextFrame(Action callbackMethod)
    {
        yield return 0;
        callbackMethod?.Invoke();
    }
    
}
