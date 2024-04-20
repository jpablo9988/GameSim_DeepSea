using System;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(ShakingAnimations))]
public class FishBehavior : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Placeholder")]
    [Range(0f, 1f)]
    private float _vSkew = 0, _hSkew = 0;

    [SerializeField]
    [Tooltip("Z Position")]
    private float _zPosition = 7;
    private Vector3 _fishPosition;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private float peekTimerLimit = 3.0f;
    [SerializeField]
    private float peekTimerVariance = 0.5f;
    [SerializeField]
    private float peekTimer = 0.0f;


    [SerializeField]
    private float peekSpeed = 10;

    [SerializeField]
    private float runAwaySpeed = 100;


    [SerializeField]
    private float runAwayDuration = 1;

    public bool WillHurtPlayer { get; set; }


    private float peekAngleLimit = 10.0f;
    private float showFishAngleLimit = 5.0f;
    private float runAwayTimer = 1;
    private bool isFishCaught = false;
    private bool willRunAway = false;
    private bool willPeek = true;
    private IEnumerator peekCoroutine;
    private IEnumerator timerCoroutine;
    private IEnumerator moveCoroutine;
    public bool WillLoseOxygen = true;

    // -- Dependencies -- //
    private SpriteRenderer fishVisuals;
    private ShakingAnimations shake;

    private void Awake()
    {
        fishVisuals = GetComponent<SpriteRenderer>();
        shake = GetComponent<ShakingAnimations>();   
    }
    private void OnEnable()
    {
        peekTimer = 0;
        HideFish(true);
        SetZPosition(_zPosition);
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
            HideFish(false);

            isFishCaught = true;
            _fishPosition = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f + _vSkew, _zPosition));
            this.transform.position = _fishPosition;
            // Fish will be on the middle, shaking.
            // Animator.Bool("Shake") = true.
            if (willRunAway)
            {
                shake.ShakeObject(true);
                timerCoroutine = TimerMethods.GeneralTimer(runAwayTimer, MoveAway);
                StartCoroutine(timerCoroutine);
            }
        }
        else if (Mathf.Abs(angle) <= peekAngleLimit && peekTimer <= 0)
        {
            if (willPeek)
            {
                Peek(isRight);
            }
        }
    }  
    public void Peek (bool isRight)
    {
        CalculatePeekingFishPosition(isRight);
        HideFish(false);
        this.transform.position = _fishPosition;
        peekCoroutine = MoveOutOfCamera(isRight);
        StartCoroutine(peekCoroutine);
        peekTimer = UnityEngine.Random.Range(peekTimerLimit - peekTimerVariance
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
    private IEnumerator MoveForATime (Vector3 direction, float time, Action<bool> function = null )
    {
        float localTimer = time;
        while (localTimer > 0)
        {
            this.transform.Translate(runAwaySpeed * Time.deltaTime * direction);
            localTimer -= Time.deltaTime;
            yield return null;
        }
        moveCoroutine = null;
        function?.Invoke(true);
    }

    //BUG: weird fish outtage.
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

        if (isRight)
        {
            localHSkew *= -1;
        }
        else
        {
            localHPosition -= 1;
        }
        _fishPosition = mainCamera.ViewportToWorldPoint(
            new Vector3 (localHPosition + localHSkew, 0.5f + _vSkew, _zPosition));

    }

    private void MoveAway()
    {
        //Stop shaking.
        shake.ShakeObject(false);
        //Call on changing movement.
        moveCoroutine = MoveForATime(Vector3.forward, runAwayDuration, HideFish);
        StartCoroutine(moveCoroutine);
    }

    public void SetZPosition(float z)
    {
        this._zPosition = z;
    }

    public void HideFish(bool hide)
    {
        if (hide)
        {
            //Stop shaking.
            shake.ShakeObject(false);
            StopAllCoroutines();
            timerCoroutine = null;
            if (isFishCaught)
            {
                isFishCaught = false;
                EventManager.Instance.FishEscapedEvent();
            }
        }
        this.fishVisuals.enabled = !hide;
    }
    public bool IsFishCaught()
    {
        return isFishCaught;
    }
    public void SetUpFishProperties(float minAnglePeek, float minAngleCaught, float timeTillRun, bool runAway, bool willPeek)
    {
        peekAngleLimit = minAnglePeek;
        this.showFishAngleLimit = minAngleCaught;
        runAwayTimer = timeTillRun;
        willRunAway = runAway;
        this.willPeek = willPeek;

    }
}
