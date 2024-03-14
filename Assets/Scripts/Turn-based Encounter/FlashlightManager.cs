using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    [SerializeField]
    private GameObject flashlightObject;

    public bool IsFlashLightActive { get; private set; }

    private void Awake()
    {
        flashlightObject.SetActive(false);
        IsFlashLightActive = false;
    }
    public void ActivateFlashlight(bool activate)
    {
        EventManager.Instance.FlashlightEvent(activate);
        flashlightObject.SetActive(activate);
        IsFlashLightActive = activate;
    }
}
