using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularRigController : MonoBehaviour
{

    private FishBehavior fish;
    [SerializeField]
    [Tooltip("FishObject")]
    private GameObject fishArea;

    [SerializeField]
    private float minRotateRange = 30.0f;

    [SerializeField]
    private float maxRotateRange = 120.0f;

    [SerializeField]
    private bool willRotate = true;
    private Transform cameraTransform;
    private CircularRigMovement movementControls;

    private void Awake()
    {
        fish = GetComponentInChildren<FishBehavior>();  
        movementControls = GetComponent<CircularRigMovement>();
        cameraTransform = Camera.main.transform;
    }
    private void Start()
    {
        if (willRotate) this.movementControls.IsRotating(true);
        //fish.SetZPosition(circleRigRadius);
    }
    private void OnEnable()
    {
        EventManager.FlashlightAction += ManageFishVisuals;
        EventManager.FishEscapedAction += RandmlyRotateRig;
    }
    private void OnDisable()
    {
        EventManager.FlashlightAction -= ManageFishVisuals;
        EventManager.FishEscapedAction -= RandmlyRotateRig;
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
                fish.ExecuteFishVisuals(true, yAngle);
            }
            else
            {
                fish.ExecuteFishVisuals(false, yAngle);
            }
        }
        else
        {
            if (willRotate)
                this.movementControls.IsRotating(true);
            fish.HideFish(true);
        }
    }
    private Vector3 GetDirectionVectorToTarget(Vector3 position, Vector3 target)
    {
        Vector3 heading = target - position;
        float distance = heading.magnitude;
        return heading / distance;
    }
    private void RandmlyRotateRig()
    {
        movementControls.ChangeRotation(minRotateRange, maxRotateRange);
    }
}
