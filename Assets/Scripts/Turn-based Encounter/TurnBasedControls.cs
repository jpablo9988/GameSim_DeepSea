using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnBasedControls : MonoBehaviour
{
    private CameraBehaviour cameraBehaviour;
    private FlashlightManager flashlight;
    private CameraFlash flash;
    [SerializeField]
    private bool controlsActive = true;
    void Awake()
    {
        cameraBehaviour = GetComponent<CameraBehaviour>();
        flashlight = GetComponent<FlashlightManager>();
        flash = GetComponentInChildren<CameraFlash>();  
    }


    void Update()
    {
        if (controlsActive)
        {
            float x = Input.GetAxis("Horizontal");

            if (!flashlight.IsFlashLightActive)
            {
                cameraBehaviour.RotateHorizontalPov(x * Time.deltaTime);
            }
            if (Input.GetButtonDown("Flashlight"))
            {
                flashlight.ActivateFlashlight(true);
            }
            if (Input.GetButtonUp("Flashlight"))
            {
                flashlight.ActivateFlashlight(false);
            }
            if (Input.GetButtonDown("TakePhoto"))
            {
                flash.ActivateCameraFlash();
            }
        }
    }
    public void ActivateControls(bool value)
    {
        flashlight.ActivateFlashlight(false);
        controlsActive = value;
    }
}
