using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    private TurnBasedControls _tbControls;
    private FPPlayerMovement _fpControls;
    private FlashlightControls _flashControls;

    public bool IsTurnBased { get; private set; }

    private bool turnBasedLocal;

    private bool InteractButton;

    void Awake()
    {
        
        _tbControls = GetComponent<TurnBasedControls>();
        _fpControls = GetComponent<FPPlayerMovement>();
        _flashControls = GetComponentInChildren<FlashlightControls>();
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
        }
    }
    private void ActivateTurnBasedControls(bool value)
    {
        IsTurnBased = value;
        turnBasedLocal = IsTurnBased;
        _tbControls.ActivateControls(value);
        _fpControls.ActivateControls(!value);

    }
    private void FinishTurnBased()
    {
        IsTurnBased = false;
        turnBasedLocal = IsTurnBased;
        PauseControls(false);
    }
    private void PauseControls(bool value)
    {
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
        this.InteractButton = !value;
    }
}
