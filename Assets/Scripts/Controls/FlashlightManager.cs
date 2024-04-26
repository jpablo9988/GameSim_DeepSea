using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    [SerializeField]
    private GameObject explorationFlashlightObject;
    [SerializeField]
    private GameObject turnBasedFlashlight;

    public bool IsFlashLightActive { get; private set; }
    public bool TurnBasedControls { get; set; }
    private void OnEnable()
    {
        TurnBasedControls = false;
        ResetFlashlight();
    }
    public void ActivateFlashlight(bool activate)
    {
        EventManager.Instance.FlashlightEvent(activate);
        if (!TurnBasedControls)
            explorationFlashlightObject.SetActive(activate);
        else
            turnBasedFlashlight.SetActive(activate);
        IsFlashLightActive = activate;
    }
    public void ResetFlashlight()
    {
        explorationFlashlightObject.SetActive(false);
        IsFlashLightActive = false;
        turnBasedFlashlight.SetActive(false);
    }
}
