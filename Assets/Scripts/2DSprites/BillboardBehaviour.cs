using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private void LateUpdate()
    {
        LookAtPlayer();
    }
    public void LookAtPlayer()
    {
        transform.LookAt(player.transform);
        transform.Rotate(0, 180, 0);
    }
    public Vector3 DirectionAtPlayer(Vector3 position)
    {
        Vector3 heading = player.transform.position - position;
        float distance = heading.magnitude;
        return heading / distance;
    }
}
