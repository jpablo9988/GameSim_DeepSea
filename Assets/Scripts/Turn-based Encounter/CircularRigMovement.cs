using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularRigMovement : MonoBehaviour
{
    private bool isRotating;
    [SerializeField]
    private float degreesPerSecond;

    void Update()
    {
        if (isRotating)
        {
            this.transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
        }
    }
    public void ChangeRotation (float minValue, float maxValue)
    {
        float newDegree = Random.Range(minValue, maxValue);
        this.transform.Rotate(new Vector3(0, newDegree, 0));
    }
    public void IsRotating(bool value)
    {
        isRotating = value;
    }
}
