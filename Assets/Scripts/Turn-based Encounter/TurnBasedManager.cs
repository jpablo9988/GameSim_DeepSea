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
    private OxygenBar bar;

    private CanvasGroup _tbUIDetails;

    [SerializeField]
    private GameObject _endScreen;

    [SerializeField]
    private bool isEmpty = false;

    private EndScreenManager _endScreenManager;

    private bool _success;

    public bool TurnBased { get; private set; }
    private void Awake()
    {
        
        _endScreenManager = _endScreen.GetComponent<EndScreenManager>();
        _tbUIDetails = _tbUI.GetComponent<CanvasGroup>();
    }
    private void OnEnable()
    {
        IsTurnedBased(false);
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
        // DO black screen, deactivate controls.
        // Once it's faded out, activate controls.
        TurnBased = value;
        if (!isEmpty)
            _arena.SetActive (value);

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
    private void TransitionToEndScreen()
    {
        if (_success)
        {
            _fish.HideFish(true);
            _endScreen.SetActive(true);
            EventManager.Instance.IsControlsPaused(true);
            _endScreenManager.ShowBlackScreen(ShowPhoto);
        }
        else
        {
            if (_fish.WillLoseOxygen)
            {
                bar.ChangeOxygen(-40);
            }
        }
    }
    private void CheckIfPhotoSuccess()
    {
        _success = _fish.IsFishCaught();
    }

    private void ShowPhoto()
    {
        _endScreenManager.ShowPhoto(CheckForInput);
    }
    private void CheckForInput()
    {
        StartCoroutine(HasPlayerInput());
    }
    public void ToExploration()
    {
        StartCoroutine(General2DMethods.FadeImage(_tbUIDetails, 1.0f, false, TurnOffTurnBased));
    }
    private void TurnOffTurnBased()
    {
        EventManager.Instance.FinishTurnBased();
        EventManager.Instance.IsControlsPaused(false);
        IsTurnedBased(false);
    }
    private IEnumerator HasPlayerInput()
    {
        bool inputTrue = false;
        while (!inputTrue)
        {
            if (Input.anyKeyDown)
            {
                inputTrue = true;
            }
            yield return null;
        }
        ToExploration();
    }
}
