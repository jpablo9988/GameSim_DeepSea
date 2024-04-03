using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _blackScreen;
    [SerializeField]
    private CanvasGroup _photo;

    private Image _blackScreenImage;

    [SerializeField]
    private float _blackScreenFadeDuration = 1.0f;

    [SerializeField]
    private float _showPhotoFadeDuration = 1.0f;
    private void Awake()
    {
        _blackScreenImage = _blackScreen.GetComponent<Image>();
    }
    private void OnEnable()
    {
        _photo.alpha = 0;
        _blackScreenImage.enabled = false;
    }
    public void ShowBlackScreen(Action callbackMethod = null)
    {
        _blackScreenImage.enabled = true;
        //Reset Black Screen Alpha to 0
        Color auxColor = _blackScreenImage.color;
        auxColor.a = 0;
        _blackScreenImage.color = auxColor;
        StartCoroutine(General2DMethods.FadeImage(_blackScreenImage, _blackScreenFadeDuration, true, callbackMethod));
    }

    public void ShowPhoto(Action callbackMethod = null)
    {
        _photo.interactable = true;
        StartCoroutine(General2DMethods.FadeImage(_photo
            , _showPhotoFadeDuration
            , true
            , callbackMethod));
    }
    public void ActivatePhoto(bool value)
    {
        _photo.interactable = value;
    }

}
