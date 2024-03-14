using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularRigController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Z Radius")]
    private float circleRigRadius = 7f;

    private FishBehavior fish;
    [SerializeField]
    [Tooltip("FishObject")]
    private GameObject fishArea;

    private Transform cameraTransform;


    private void Awake()
    {
        fish = GetComponentInChildren<FishBehavior>();  
        cameraTransform = Camera.main.transform;
    }
    private void Start()
    {
        fish.SetZPosition(circleRigRadius);
    }
    private void OnEnable()
    {
        EventManager.FlashlightAction += ManageFishVisuals;
    }
    private void OnDisable()
    {
        EventManager.FlashlightAction -= ManageFishVisuals;
    }
    public void ManageFishVisuals(bool isTurned)
    {
        if (isTurned)
        {
  
            Vector3 fishAreaDirection = GetDirectionVectorToTarget(cameraTransform.position
                , fishArea.transform.position);
            Vector3 fowardDirection = cameraTransform.forward;
            float yAngle = Vector3.SignedAngle(fishAreaDirection, fowardDirection, Vector3.up) * -1;
            if (yAngle > 0)
            {
                fish.ExecuteFishVisuals(true, yAngle);
            }
            else
            {
                fish.ExecuteFishVisuals(false, yAngle);
            }
        }
        else
        {
            fish.HideFish(true);
        }
    }
    private Vector3 GetDirectionVectorToTarget(Vector3 position, Vector3 target)
    {
        Vector3 heading = target - position;
        float distance = heading.magnitude;
        return heading / distance;
    }
}
