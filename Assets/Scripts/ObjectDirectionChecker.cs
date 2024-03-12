using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectDirectionChecker : MonoBehaviour
{
    public Transform player;
    public Transform objectToCheck;

    void Update()
    {

        Vector3 playerPosition = player.position;
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
}