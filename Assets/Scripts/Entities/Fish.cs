using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    public string nameFish;
    public Texture2D texture;
    [SerializeField]
    [Range(0, 180)]
    private float minAngleFishPeek = 10.0f;

    [SerializeField]
    [Range(0, 180)]
    private float minAngleFishCaught = 5.0f;

    [SerializeField]
    private float timeUntilRunAway = 1;

    [SerializeField]
    private bool isTriggerAreaMoving = false;

    [SerializeField]
    private bool willMoveOnFail = false;

    [SerializeField]
    private bool willHitPlayerOnFail = false;

    [SerializeField]
    private bool willRunAway = true;

    [SerializeField]
    private bool willPeek = true;

    [SerializeField]
    private float triggerAreaRotateVelocity = 10.0f;

    [SerializeField]
    private TextMeshProUGUI nameField;


    

    [SerializeField]
    private RawImage portaitLocation;
    //Get Dependencies.
    private CircularRigController rigController;
    private FishBehavior fishBehavior;

    private void Awake()
    {
        rigController = GetComponent<CircularRigController>();
        fishBehavior = GetComponentInChildren<FishBehavior>();
    }
    private void Start()
    {
        // -- Set-up properties -- //
        rigController.IsCircularRigRotating(isTriggerAreaMoving, triggerAreaRotateVelocity);
        rigController.WillRandomlyRotate(willMoveOnFail);
        fishBehavior.SetUpFishProperties(
            minAngleFishPeek, minAngleFishCaught, timeUntilRunAway, willRunAway, willPeek);
        fishBehavior.WillHurtPlayer = willHitPlayerOnFail;

        // -- Set-up Portait -- //
        portaitLocation.texture = texture;
        nameField.text = nameFish;
    }
}
