using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularRigController : MonoBehaviour
{

    private FishBehavior fishBehavior;
    [SerializeField]
    [Tooltip("FishObject")]
    private GameObject fishArea;

    [SerializeField]
    private float minRotateRange = 30.0f;

    [SerializeField]
    private float maxRotateRange = 120.0f;

    private bool willRotate;
    private Transform cameraTransform;
    private CircularRigMovement movementControls;
    private bool willRandomlyRotate = false;

    private void Awake()
    {
        fishBehavior = GetComponentInChildren<FishBehavior>();  
        movementControls = GetComponent<CircularRigMovement>();
        cameraTransform = Camera.main.transform;
    }
    private void OnEnable()
    {
        EventManager.FlashlightAction += ManageFishVisuals;
        EventManager.FishEscapedAction += RandomlyRotateRig;
    }
    private void OnDisable()
    {
        EventManager.FlashlightAction -= ManageFishVisuals;
        EventManager.FishEscapedAction -= RandomlyRotateRig;
    }
    public void ManageFishVisuals(bool isTurned)
    {
        if (isTurned)
        {
  
            Vector3 fishAreaDirection = GetDirectionVectorToTarget(cameraTransform.position
                , fishArea.transform.position);
            Vector3 fowardDirection = cameraTransform.forward;
            float yAngle = Vector3.SignedAngle(fishAreaDirection, fowardDirection, Vector3.up) * -1;
            if (willRotate) this.movementControls.IsRotating(false);
            if (yAngle > 0)
            {
                fishBehavior.ExecuteFishVisuals(true, yAngle);
            }
            else
            {
                fishBehavior.ExecuteFishVisuals(false, yAngle);
            }
        }
        else
        {
            if (willRotate)
                this.movementControls.IsRotating(true);
            fishBehavior.HideFish(true);
        }
    }
    private Vector3 GetDirectionVectorToTarget(Vector3 position, Vector3 target)
    {
        Vector3 heading = target - position;
        float distance = heading.magnitude;
        return heading / distance;
    }
    private void RandomlyRotateRig()
    {
        if (willRandomlyRotate)
        {
            movementControls.ChangeRotation(minRotateRange, maxRotateRange);
        }
    }
    public void IsCircularRigRotating(bool value, float velocity = 0)
    {
        willRotate = value;
        movementControls.IsRotating(value);
        if (velocity != 0) movementControls.SetVelocity(velocity);
    }
    public void WillRandomlyRotate(bool value)
    {
        willRandomlyRotate = value;
    }
}
