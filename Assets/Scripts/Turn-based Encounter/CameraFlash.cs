using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlash : MonoBehaviour
{
    [SerializeField]
    private float flashLightDuration;

    [SerializeField]
    private float gracePeriod;

    [SerializeField]
    private float cameraCooldown = 2.0f;

    private float currCameraCooldown;

    private Light cameraFlash;

    private void Awake()
    {
       
        cameraFlash = GetComponentInChildren<Light>();
        
    }
    private void OnEnable()
    {
        currCameraCooldown = 0.0f;
        cameraFlash.enabled = false;
    }
    private void Update()
    {
        if (currCameraCooldown > 0)
        {
            currCameraCooldown -= Time.deltaTime;
        }
    }
    public void ActivateCameraFlash()
    {
        if (currCameraCooldown <= 0)
        {
            currCameraCooldown = cameraCooldown;
            cameraFlash.enabled = true;
            StartCoroutine(TimerMethods.GeneralTimer(flashLightDuration, TurnOffFlash));
            EventManager.Instance.PrePhotoCheck();
            
        }
    }
    private void TurnOffFlash()
    {
        cameraFlash.enabled = false;
        StartCoroutine(TimerMethods.GeneralTimer(flashLightDuration, GracePeriodEnd));
    }
    private void GracePeriodEnd()
    {
        EventManager.Instance.TakePhoto();
    }

}
