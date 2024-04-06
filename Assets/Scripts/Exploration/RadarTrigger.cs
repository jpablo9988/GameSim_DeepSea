using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform fishLocation;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ObjectDirectionChecker.Radar.SetObject(fishLocation);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ObjectDirectionChecker.Radar.FinishLocatingObject();
        }
    }
}
