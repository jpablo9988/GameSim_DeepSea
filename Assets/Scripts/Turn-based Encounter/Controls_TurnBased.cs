using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controls_TurnBased : MonoBehaviour
{
    private CameraBehaviour cameraBehaviour;
    private FlashlightManager flashlight;
    void Awake()
    {
        cameraBehaviour = GetComponent<CameraBehaviour>();
        flashlight = GetComponent<FlashlightManager>();
    }


    void Update()
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
    }
}
