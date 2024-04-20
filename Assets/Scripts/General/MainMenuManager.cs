using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip menuMusic;
    [SerializeField]
    private float fadeDuration = 1.0f;
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private GameObject confirmationPanel;
    [SerializeField]
    private GameObject settingsMenu;
    [SerializeField]
    private GameObject mainMenu;

    private bool playerExists;
    private void OnEnable()
    {
        MusicManager.Instance.PlayAudio(menuMusic, fadeDuration);
        Cursor.lockState = CursorLockMode.None;
        CheckSaveData();
        EventManager.DialogueDone += StartBlankSlateGame;
    }
    private void OnDisable()
    {
        EventManager.DialogueDone -= StartBlankSlateGame;
    }
    private void CheckSaveData()
    {
        if (SaveManager.Instance.GetSaveState() == SaveStates.NEW)
        {
            TextMeshProUGUI text = continueButton.GetComponentInChildren<TextMeshProUGUI>();
            continueButton.interactable = false;
            playerExists = false;
            text.color = Color.gray;
        }
        else
        {
            continueButton.interactable = true;
            playerExists = true;
        }
    }
    public void StartNewGame(bool fromConfirmationPanel)
    {
        if (playerExists && !fromConfirmationPanel)
        {
            confirmationPanel.SetActive(true);
        }
        else
        {
            MusicManager.Instance.StopAudio(fadeDuration);
            mainMenu.SetActive(false);
            StoryDirector.Instance.CallStory("Introduction");
        }
    }
    private void StartBlankSlateGame()
    {
        GameManager.Instance.StartGame();
    }
    public void ContinueGame()
    {
        MusicManager.Instance.StopAudio(fadeDuration);
        GameManager.Instance.ContinueGame();
    }
    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
    public void Settings()
    {
        settingsMenu.SetActive(true);
    }
    public void Gallery()
    {
        confirmationPanel.SetActive(true);

    }

}
