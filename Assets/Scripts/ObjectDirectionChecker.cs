using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectDirectionChecker : MonoBehaviour
{
    public Transform Player;
    [SerializeField]
    private Transform objectToCheck;

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
    }
    void Update()
    {
        if (willDetectObject && !isInTopOfObject)
        {
            Vector3 playerPosition = Player.position;
            Vector3 objectPosition = objectToCheck.position;

            Vector3 direction = objectPosition - playerPosition;

            string directionText = "";

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
            {
                directionText = (direction.x > 0) ? "East" : "West";
            }
            else
            {
                directionText = (direction.z > 0) ? "North" : "South";
            }

            Debug.Log("Player is standing " + directionText + " of the object.");
        }
        else if (isInTopOfObject)
        {
            Debug.Log("Player is on top of object");
        }
    }
    public void SetObject(Transform transform)
    {
        isInTopOfObject = false;
        willDetectObject = true;
        this.objectToCheck = transform;
    }
    public void FinishLocatingObject()
    {
        isInTopOfObject = false;
        willDetectObject = false;
        this.objectToCheck = null;
    }
    public void PlayerOnObject(bool value)
    {
        isInTopOfObject = value;
    }
}