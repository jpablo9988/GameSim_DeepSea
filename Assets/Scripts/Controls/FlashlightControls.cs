using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightControls : MonoBehaviour
{
    private FlashlightManager flashlight;
    [SerializeField]
    private bool controlsActive = true;

    void Awake()
    {
        flashlight = GetComponent<FlashlightManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controlsActive)
        {
            if (Input.GetButtonDown("Flashlight"))
            {
                flashlight.ActivateFlashlight(true);
            }
            if (Input.GetButtonUp("Flashlight"))
            {
                flashlight.ActivateFlashlight(false);
            }
        }
    }
    public void ActivateControls(bool value)
    {
        controlsActive = value;
    }

    public bool IsFlashLightActive()
    {
        return flashlight.IsFlashLightActive;
    }
    public void FlashlightTurnBased(bool value)
    {
        flashlight.TurnBasedControls = value;
    }
    public void TurnOffFlashlight()
    {
        flashlight.ResetFlashlight();
    }
}
