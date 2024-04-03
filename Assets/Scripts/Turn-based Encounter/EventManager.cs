using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    public delegate void NoParamsAction();
    public delegate void BoolAction(bool value);

    public static event BoolAction FlashlightAction;
    public static event NoParamsAction FishEscapedAction;
    public static event BoolAction PauseGame;
    public static event NoParamsAction PhotoTaken;
    public static event NoParamsAction WillTakePhoto;
    public static event BoolAction PauseControls;
    public static event BoolAction Interacted;
    public static event NoParamsAction TurnBasedDone;

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
    public void FishEscapedEvent()
    {
        FishEscapedAction?.Invoke();
    }
    public void SetGamePaused(bool isPaused)
    {
        PauseGame?.Invoke(isPaused);
    }
    public void TakePhoto()
    {
        PhotoTaken?.Invoke();
    }
    public void PrePhotoCheck()
    {
        WillTakePhoto?.Invoke();
    }
    public void InteractWithSomething(bool value)
    {
        Interacted?.Invoke(value);
    }

    public void FinishTurnBased()
    {
        TurnBasedDone?.Invoke();
    }

    public void IsControlsPaused(bool value)
    {
        PauseControls?.Invoke(value);
    }
}
