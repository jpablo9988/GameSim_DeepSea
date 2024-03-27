using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _arena;
    [SerializeField]
    private GameObject _tbUI;
    [SerializeField]
    private FishBehavior _fish;
    [SerializeField]
    private Controls_TurnBased _tbControls;

    public void IsTurnedBased (bool value)
    {
        _arena.SetActive (value);
        _tbControls.ActivateControls(value);
        _tbUI.SetActive (value);
    }
}
