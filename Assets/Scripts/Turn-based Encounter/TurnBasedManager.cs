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
    private TurnBasedControls _tbControls;

    private CanvasGroup _tbUIDetails;

    [SerializeField]
    private GameObject _endScreen;

    private EndScreenManager _endScreenManager;

    private bool _success;

    public bool TurnBased { get; private set; }
    private void Awake()
    {
        //Debug!
        TurnBased = true;
        _endScreenManager = _endScreen.GetComponent<EndScreenManager>();
        _tbUIDetails = _tbUI.GetComponent<CanvasGroup>();
    }
    private void OnEnable()
    {
        EventManager.PhotoTaken += TransitionToEndScreen;
        EventManager.WillTakePhoto += CheckIfPhotoSuccess;
    }
    private void OnDisable()
    {
        EventManager.PhotoTaken -= TransitionToEndScreen;
        EventManager.WillTakePhoto -= CheckIfPhotoSuccess;

    }
    public void IsTurnedBased (bool value)
    {
        TurnBased = value;
        _arena.SetActive (value);
        _tbControls.ActivateControls(value);
        _tbUI.SetActive (value);
        if (value) _tbUIDetails.alpha = 1;
        else _tbUIDetails.alpha = 0;
        _tbUIDetails.interactable = value;
        if (_endScreen.activeInHierarchy && value)
            _endScreen.SetActive (false);
        if (value)
        {
            _success = false;
            _endScreenManager.ActivatePhoto(false);
        }
    }
    public void ActiveInteractables(bool value)
    {
        if (TurnBased)
        {
            _tbControls.ActivateControls(value);
            _tbUIDetails.interactable = value;
        }
    }
    
    private void TransitionToEndScreen()
    {
        if (_success)
        {
            _fish.HideFish(true);
            _endScreen.SetActive(true);
            _tbControls.ActivateControls(false);
            _endScreenManager.ShowBlackScreen(ShowPhoto);
        }
        else
        {
            Debug.Log("Fail. Lose Oxygen");
        }
    }
    private void CheckIfPhotoSuccess()
    {
        _success = _fish.IsFishCaught();
    }

    private void ShowPhoto()
    {
        _endScreenManager.ShowPhoto();
    }

    public void ToExploration()
    {
        StartCoroutine(General2DMethods.FadeImage(_tbUIDetails, 1.0f, false, TurnOffTurnBased));
    }
    private void TurnOffTurnBased()
    {
        IsTurnedBased(false);
    }
}
