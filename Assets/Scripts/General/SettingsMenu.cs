using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Toggle toggle;
    private void OnEnable()
    {
        slider.value = Settings.Volume;
        toggle.isOn = Screen.fullScreen;
    }

    public void SetVolume (float volume)
    {
        Settings.Volume = volume;
        MusicManager.Instance.RefreshVolume();
        SaveManager.Instance.SaveVolumeSettings();
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void GoBack()
    {
        if (PauseMenuManager.Instance != null)
            PauseMenuManager.Instance.ActivateSettingsTab(false);
        else
            this.gameObject.SetActive(false);
    }
}
