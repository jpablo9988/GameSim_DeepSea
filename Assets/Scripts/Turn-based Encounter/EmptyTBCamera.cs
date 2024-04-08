using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singleton!
public class EmptyTBCamera : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private GameObject turnBasedArena;
    private TurnBasedManager emptyCameraManager;
    public static EmptyTBCamera Instance { get; private set; }
    public bool Overwriten { get; set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is an extra Empty arena. ErroR!");
            Destroy(this);
        }
        emptyCameraManager = turnBasedArena.GetComponent<TurnBasedManager>();
        turnBasedArena.SetActive(false);
    }
    private void OnEnable()
    {
        EventManager.Interacted += PullEmptyArena;
    }
    private void OnDisable()
    {
        EventManager.Interacted -= PullEmptyArena;
    }

    private void PullEmptyArena(bool value)
    {
        if (!Overwriten)
        {
            if(value)
                turnBasedArena.transform.position = player.position;
            turnBasedArena.SetActive(value);
            emptyCameraManager.IsTurnedBased(value);
        }
    }
}
