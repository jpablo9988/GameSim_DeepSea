using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTrigger : MonoBehaviour
{
    [SerializeField]
    private TurnBasedManager fishArena;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EmptyTBCamera.Instance.Overwriten = true;
            if (fishArena != null)
            {
                EventManager.Interacted += fishArena.IsTurnedBased;
            }
            Debug.Log("Player triggered event");
        }
    }

    private void OnTriggerExit(Collider other)
    {
      if(other.gameObject.CompareTag("Player"))
        {
            EmptyTBCamera.Instance.Overwriten = false;
            if (fishArena != null)
            {
                EventManager.Interacted -= fishArena.IsTurnedBased;
            }
            Debug.Log("Player exited event");
        }
    }
}
