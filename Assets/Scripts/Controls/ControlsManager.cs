using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    private TurnBasedControls _tbControls;
    private FPPlayerMovement _fpControls;
    private FlashlightControls _flashControls;
    private MouseLook _mouseManager;
    public bool IsTurnBased { get; private set; }

    private bool turnBasedLocal;

    private bool InteractButton;

    void Awake()
    {
        _tbControls = GetComponent<TurnBasedControls>();
        _fpControls = GetComponent<FPPlayerMovement>();
        _flashControls = GetComponentInChildren<FlashlightControls>();
        _mouseManager = GetComponentInChildren<MouseLook>();
    }
    private void OnEnable()
    {
        EventManager.Interacted += ActivateTurnBasedControls;
        EventManager.PauseControls += PauseControls;
        EventManager.TurnBasedDone += FinishTurnBased;
    }
    private void OnDisable()
    {
        EventManager.Interacted -= ActivateTurnBasedControls;
        EventManager.PauseControls -= PauseControls;
        EventManager.TurnBasedDone -= FinishTurnBased;
    }
    private void Start()
    {
        turnBasedLocal = false;
        IsTurnBased = false;
        InteractButton = true;
    }
    private void Update()
    {
        if (InteractButton)
        {
            if (Input.GetButtonDown("Interact"))
            {
                turnBasedLocal = !turnBasedLocal;
                EventManager.Instance.InteractWithSomething(turnBasedLocal);
            }
            if (Input.GetButtonDown("Pause"))
            {
                GameStateManager.Instance.PauseGame();
            }
        }
        else
        {
            if (Input.GetButtonDown("Pause"))
            {
                GameStateManager.Instance.UnpauseGame();
            }
        }
    }
    private void ActivateTurnBasedControls(bool value)
    {
        IsTurnBased = value;
        turnBasedLocal = IsTurnBased;
        _flashControls.TurnOffFlashlight();
        _flashControls.FlashlightTurnBased(value);
        _tbControls.ActivateControls(value);
        _fpControls.ActivateControls(!value);

    }
    private void FinishTurnBased()
    {
        _flashControls.TurnOffFlashlight();
        IsTurnBased = false;
        turnBasedLocal = IsTurnBased;
        PauseControls(false);
        _flashControls.FlashlightTurnBased(false);
    }
    public void PauseControls(bool value)
    {
        InteractButton = !value;
        _flashControls.ActivateControls(!value);
        _mouseManager.IsMouseLocked(!value);
        if (!value)
        {
            if (IsTurnBased)
            {
                _tbControls.ActivateControls(!value);
            }
            else
            {
                _fpControls.ActivateControls(!value);
            }
        }
        else
        {
            _tbControls.ActivateControls(!value);
            _fpControls.ActivateControls(!value);
        }
    }
}
