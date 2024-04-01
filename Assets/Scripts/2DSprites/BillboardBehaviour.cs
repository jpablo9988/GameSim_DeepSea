using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardBehaviour : MonoBehaviour
{
    Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void LateUpdate()
    {
        LookAtPlayer();
    }
    public void LookAtPlayer()
    {
        transform.LookAt(mainCamera.transform);
        transform.Rotate(0, 180, 0);
    }
    public Vector3 DirectionAtPlayer(Vector3 position)
    {
        Vector3 heading = mainCamera.transform.position - position;
        float distance = heading.magnitude;
        return heading / distance;
    }
}
