using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenManager : MonoBehaviour
{
    CanvasGroup imageManager;
    void Awake()
    {
        if (!TryGetComponent(out imageManager))
        {
            imageManager = GetComponentInChildren<CanvasGroup>();
        }
        SetFirstSettings();
    }
    private void SetFirstSettings()
    {
        imageManager.alpha = 0;
        imageManager.blocksRaycasts = false;
    }
    public void FadeInLoadingScreen()
    {
        imageManager.blocksRaycasts = true;
        General2DMethods.FadeImage(imageManager, 1.0f, true);
    }
    public void FadeOutLoadingScreen()
    {
        General2DMethods.FadeImage(imageManager, 1.0f, false, FinishFadingOut);
    }
    private void FinishFadingOut()
    {
        imageManager.blocksRaycasts = false;
    }
}
