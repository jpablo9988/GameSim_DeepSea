using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectDirectionChecker : MonoBehaviour
{
    public Transform Player;
    [SerializeField]
    private Transform objectToCheck;
    [SerializeField]
    [Range(0f, 1f)]
    private float angleLimit;

    private RadarAnimations animManager;

    private bool willDetectObject;
    private bool isInTopOfObject;
    public static ObjectDirectionChecker Radar;
    private void Awake()
    {
        if (Radar == null)
        {
            Radar = this;
        }
        else
        {
            Debug.LogWarning("There are multiple Object Direction Checkers. Bad");
            Destroy(this);
        }
        if (!TryGetComponent(out animManager))
        {
            animManager = GetComponentInChildren<RadarAnimations>();  
        }
    }
    void Update()
    {
        if (willDetectObject && !isInTopOfObject)
        {
            Vector3 playerPosition = Player.position;
            Vector3 objectPosition = objectToCheck.position;
            Vector3 direction = playerPosition - objectPosition;
            SetAnimations(direction);
        }
        else if (isInTopOfObject)
        {
            SetAlert();
        }
    }
    private void SetAnimations(Vector3 direction)
    {
        float distH = Mathf.Abs(direction.x), distV = Mathf.Abs(direction.z);
        float X = direction.x / (distH + distV);
        float Z = direction.z / (distH + distV);
        animManager.SetDirections(X, Z);
    }
    private void SetAlert()
    {
        animManager.SetDirections(0, 0);
    }
    private void ActivateAnims(bool value)
    {
        animManager.IsRadarOn(value);
    }
    public void SetObject(Transform transform)
    {
        isInTopOfObject = false;
        willDetectObject = true;
        ActivateAnims(true);
        this.objectToCheck = transform;
    }
    public void FinishLocatingObject()
    {
        isInTopOfObject = false;
        willDetectObject = false;
        ActivateAnims(false);
        this.objectToCheck = null;
    }
    public void PlayerOnObject(bool value)
    {
        isInTopOfObject = value;
    }
}