using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip menuMusic;
    [SerializeField]
    private float fadeDuration = 1.0f;
    private void Start()
    {
        MusicManager.Instance.PlayAudio(menuMusic, fadeDuration);
    }
    public void StartNewGame()
    {
        //If Continue -> ACTIVE. (if save data has been detected).
        //Pull up confirmation panel.
        //ELSE --
        MusicManager.Instance.StopAudio(fadeDuration);
        GameManager.Instance.LoadScene(SceneIndex.TITLE_SCREEN, SceneIndex.ARENA, true);
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
        Debug.Log("You are in the settings!!");
    }
    public void Gallery()
    {
        Debug.Log("You are in the gallery!!");

    }

}
