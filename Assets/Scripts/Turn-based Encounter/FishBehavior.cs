using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Placeholder")]
    [Range(0f, 1f)]
    private float _vSkew = 0, _hSkew = 0;

    [SerializeField]
    [Tooltip("Z Position")]
    private float _zPosition = 7;
    private Vector3 _peekFishPosition;

    private Camera mainCamera;

    [SerializeField]
    private float peekTimerLimit = 3.0f;
    [SerializeField]
    private float peekTimerVariance = 0.5f;
    [SerializeField]
    private float peekTimer = 0.0f;

    [SerializeField]
    [Range(0, 180)]
    private float peekAngleLimit = 10.0f;

    [SerializeField]
    [Range(0, 180)]
    private float showFishAngleLimit = 5.0f;

    [SerializeField]
    private float peekSpeed = 10;

    private IEnumerator peekCoroutine;

    private SpriteRenderer fishVisuals;
    private void Awake()
    {
       
        mainCamera = Camera.main;
        fishVisuals = GetComponent<SpriteRenderer>();
        
    }
    private void Start()
    {
        peekTimer = 0;
        HideFish(true);
    }
    private void Update()
    {
        if (peekTimer > 0)
        {
            peekTimer -= Time.deltaTime;
        }
    }
    public void ExecuteFishVisuals(bool isRight, float angle)
    {
        if (Mathf.Abs(angle) <= showFishAngleLimit)
        {
            // Fish will be on the middle, shaking.
            // After a few seconds, go back.
        }
        else if (Mathf.Abs(angle) <= peekAngleLimit && peekTimer <= 0)
        {
            Peek(isRight);
        }
    }  
    public void Peek (bool isRight)
    {
        CalculatePeekingFishPosition(isRight);
        HideFish(false);
        this.transform.position = _peekFishPosition;
        peekCoroutine = MoveOutOfCamera(isRight);
        StartCoroutine(peekCoroutine);
        peekTimer = Random.Range(peekTimerLimit - peekTimerVariance
           , peekTimerLimit + peekTimerVariance);
    }
    private IEnumerator MoveOutOfCamera(bool isRight)
    {
        bool outOfCamera = false;
        while (!outOfCamera)
        {
            if (isRight)
            {
                this.transform.Translate(peekSpeed * Time.deltaTime * Vector3.right);
            }
            else
            {
                this.transform.Translate(peekSpeed * Time.deltaTime * Vector3.left);
            }
            // Check if fish is out of camera.
            outOfCamera = IsFishOutOfCamera(isRight);
            yield return null;
        }
        peekCoroutine = null;
        HideFish(true);
    }
    private bool IsFishOutOfCamera(bool isRight)
    {
        bool isOutOfCamera = false;
        Vector3 fishWorldPosition = transform.position;
        if (isRight)
            fishWorldPosition.x -= (fishVisuals.size.x / 2);
        else
            fishWorldPosition.x += (fishVisuals.size.x / 2);
        Vector3 fishPositionViewpoint = mainCamera.WorldToViewportPoint(fishWorldPosition);
        if (Mathf.Abs(fishPositionViewpoint.x) > 1)
        {
            isOutOfCamera = true;
        }
        return isOutOfCamera;
    }
    private void CalculatePeekingFishPosition(bool isRight)
    {
        float localHSkew = _hSkew, localHPosition = 1;

        if (!isRight)
        {
            localHSkew *= -1;
        }
        else
        {
            localHPosition -= 1;
        }
        _peekFishPosition = mainCamera.ViewportToWorldPoint(
            new Vector3 (localHPosition + localHSkew, 0.5f + _vSkew, _zPosition));

    }

    public void SetZPosition(float z)
    {
        this._zPosition = z;
    }

    public void HideFish(bool hide)
    {
        if (peekCoroutine != null && hide)
        {
            StopCoroutine(peekCoroutine);
        }
        this.fishVisuals.enabled = !hide;
    }


}
