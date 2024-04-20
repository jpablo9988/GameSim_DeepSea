using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    //-- Visuals Information -- //
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public bool OxygenActive { get; set; }
    private void Awake()
    {
        OxygenActive = false;
        SetMaxOxygen(180);
    }
    private void Update()
    {
        if (OxygenActive)
        {
            ChangeOxygen(-Time.deltaTime);
        }
    }
    public void SetMaxOxygen(float oxygen)
    {
        slider.maxValue = oxygen;
        slider.value = oxygen;
        fill.color = gradient.Evaluate(1f);
    }
    public void SetOxygen (float oxygen)
    {
        slider.value = oxygen;
    }
    public float GetMaxOxygen()
    {
        return slider.maxValue;
    }
    public float GetCurrentOxygen()
    {
        return slider.value;
    }

    public void ChangeOxygen(float amount)
    {
        float currentTime = slider.value + amount;
        float lerpValue = currentTime / slider.maxValue;

        slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, lerpValue);
        fill.color = gradient.Evaluate(slider.normalizedValue);
        if (slider.value <= 0f)
        {
            OxygenActive = false;
            // -- For Testing. End Game -- //
            SaveManager.Instance.ResetSavedData();
            GameManager.Instance.LoadScene(SceneIndex.ARENA, SceneIndex.THE_END, true);
        }
    }

   
}
