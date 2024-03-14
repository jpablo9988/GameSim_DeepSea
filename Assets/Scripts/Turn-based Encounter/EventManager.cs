using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    public delegate void NoParamsAction();
    public delegate void BoolAction(bool value);

    public static event BoolAction FlashlightAction;

    public static EventManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("There's a duplicate Event Manager. Destroying current.");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        
    }

    public void FlashlightEvent(bool isTurned)
    {
        FlashlightAction?.Invoke(isTurned);
    }
}
