using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnBasedControls : MonoBehaviour
{
    private TurnBasedCameraBehavior cameraBehaviour;
    private CameraFlash flash;
    [SerializeField]
    private bool controlsActive = true;

    private FlashlightControls flashControls;
    void Awake()
    {
        cameraBehaviour = GetComponent<TurnBasedCameraBehavior>();
        flashControls = GetComponentInChildren<FlashlightControls>();
        flash = GetComponentInChildren<CameraFlash>();  
    }
    void Update()
    {
        if (controlsActive)
        {
            float x = Input.GetAxis("Horizontal");
            if (!flashControls.IsFlashLightActive())
            {
                cameraBehaviour.RotateHorizontalPov(x * Time.deltaTime);
            }
            if (Input.GetButtonDown("TakePhoto"))
            {
                flash.ActivateCameraFlash();
            }
        }
    }
    public void ActivateControls(bool value)
    {
        flashControls.ChangeFlashlightState(false);
        controlsActive = value;
    }
}
