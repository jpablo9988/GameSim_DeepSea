using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public static class General2DMethods
{

    //Fades a 2D Image: 
    public static IEnumerator FadeImage(Image sprite, float fadeDuration, bool FadeIn, Action callbackMethod = null)
    {

        float duration = fadeDuration;
        float alphaTimer = 0.0f;
        float currAlpha = 1.0f, endingAlpha = 0;
        if (FadeIn)
        {
            currAlpha = 0;
            endingAlpha = 1.0f;
        }
        Color currColor = sprite.color;
        while (duration > 0)
        {
            currAlpha = Mathf.Lerp(currAlpha, endingAlpha, alphaTimer);
            currColor.a = currAlpha;
            sprite.color = currColor;
            duration -= Time.deltaTime;
            alphaTimer = 1 - (duration / fadeDuration);
            yield return null;
        }
        callbackMethod?.Invoke();
    }
    public static IEnumerator FadeImage(SpriteRenderer sprite, float fadeDuration, bool FadeIn, Action callbackMethod = null)
    {
        float duration = fadeDuration;
        float alphaTimer = 0.0f;
        float currAlpha = 1.0f, endingAlpha = 0;
        if (FadeIn)
        {
            currAlpha = 0;
            endingAlpha = 1.0f;
        }
        Color currColor = sprite.color;
        while (duration > 0)
        {
            currAlpha = Mathf.Lerp(currAlpha, endingAlpha, alphaTimer);
            currColor.a = currAlpha;
            sprite.color = currColor;
            duration -= Time.deltaTime;
            alphaTimer = 1 - (duration / fadeDuration);
            yield return null;
        }
        callbackMethod?.Invoke();
    }
    public static IEnumerator FadeImage(CanvasGroup sprite, float fadeDuration, bool FadeIn, Action callbackMethod = null)
    {
        float duration = fadeDuration;
        float alphaTimer = 0.0f;
        float currAlpha = 1.0f, endingAlpha = 0;
        if (FadeIn)
        {
            currAlpha = 0;
            endingAlpha = 1.0f;
        }
        while (duration > 0)
        {
            currAlpha = Mathf.Lerp(currAlpha, endingAlpha, alphaTimer);
            sprite.alpha = currAlpha;
            duration -= Time.deltaTime;
            alphaTimer = 1 - (duration / fadeDuration);
            yield return null;
        }
        callbackMethod?.Invoke();
    }
}

