using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTrigger : MonoBehaviour
{
    [SerializeField]
    private TurnBasedManager fishArena;
    private Transform player;
    private Transform fishArenaLocation;
    private void Awake()
    {
        fishArenaLocation = fishArena.gameObject.transform;
        fishArena.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EmptyTBCamera.Instance.Overwriten = true;
            if (player == null)
                player = other.gameObject.transform;
            if (fishArena != null)
            {
                EventManager.Interacted += ActivateTurnBased;
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
                EventManager.Interacted -= ActivateTurnBased;
            }
            Debug.Log("Player exited event");
        }
    }
    private void ActivateTurnBased(bool value)
    {
        fishArenaLocation = player;
        fishArena.gameObject.SetActive(value);
        fishArena.IsTurnedBased(value);
    }
}
