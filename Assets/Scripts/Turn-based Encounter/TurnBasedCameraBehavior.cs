using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedCameraBehavior : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How fast the camera rotates")]
    [Range(1f, 100f)]
    private int rotationSpeed = 10;

    private Vector3 look;

    void Awake()
    {
        look = transform.eulerAngles;
    }
    public void RotateHorizontalPov(float axis)
    {
        look.y += axis * rotationSpeed;
        transform.eulerAngles = look;
    }
    public void UpdateRotation()
    {
        look = transform.eulerAngles;
    }
}
