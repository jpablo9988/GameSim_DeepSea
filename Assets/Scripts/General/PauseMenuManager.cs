using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager Instance;
    // Start is called before the first frame update
    [SerializeField]
    [Tooltip("References to Pause & Settings Instances")]
    private GameObject pauseMenu, settingsMenu;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
            Debug.LogWarning("There are more than two pause menus on scenes. Beware!");
        }
    }
    // Update is called once per frame
    public void ActivatePauseMenu(bool value)
    {
        pauseMenu.SetActive(value);
        if (settingsMenu.activeInHierarchy)
        {
            settingsMenu.SetActive(value); 
        }
    }
    public void ActivateSettingsTab(bool value)
    {
        pauseMenu.SetActive(!value);
        settingsMenu.SetActive(value);

    }
}
