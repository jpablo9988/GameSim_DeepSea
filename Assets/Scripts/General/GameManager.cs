using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//SINGLETON CLASS : ^)
public class GameManager : MonoBehaviour
{
    public GameStates State { get; private set; }
    public static GameManager Instance { get; private set; }
    [SerializeField]
    private GameObject loadingScreen;
    private LoadingScreenManager loadingScreenManager;
    public bool IsDoneLoading;
    List<AsyncOperation> scenesLoading = new();
    void Awake()
    {
        State = GameStates.Playing;
        if (Instance == null) Instance = this;
        else
        {
            Debug.LogWarning("There are more than one GameManager instances in scene. Beware, traveler!");
            Destroy(this);
        }
        // -- Load main Title Screen -- //
        SceneManager.LoadSceneAsync((int)SceneIndex.TITLE_SCREEN, LoadSceneMode.Additive);
        loadingScreenManager = loadingScreen.GetComponent<LoadingScreenManager>();
        IsDoneLoading = true;
    }

    public void SwitchGameStates(GameStates state)
    {
        State = state;
        switch (state)
        {
            case GameStates.Paused:
                EventManager.Instance.SetGamePaused(true);
                Time.timeScale = 0;
                break;
            case GameStates.Playing:
                EventManager.Instance.SetGamePaused(false);
                Time.timeScale = 1;
                break;
        }
    }
    public void LoadScene(SceneIndex unloadScene, SceneIndex loadScene, bool withLoadingScreen, Action onComplete = null)
    {
        if (withLoadingScreen)
            loadingScreenManager.FadeInLoadingScreen();

        scenesLoading.Add(SceneManager.LoadSceneAsync((int)loadScene, LoadSceneMode.Additive));
        if (withLoadingScreen)
        {
            StartCoroutine(GetProgressLoadScene((int)unloadScene, onComplete));
        }
    }
    public void UnloadScene(SceneIndex unloadScene)
    {
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)unloadScene));
    }
    public void AddAdditiveScene(SceneIndex loadScene)
    {
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)loadScene, LoadSceneMode.Additive));
    }
    public void StartGame()
    {
        // -- Load Checkpoints --//
        LoadScene(SceneIndex.TITLE_SCREEN, SceneIndex.ARENA, true, RelayStartGame);
    }
    public void ContinueGame()
    {
        // -- Load Checkpoints --//
        LoadScene(SceneIndex.TITLE_SCREEN, SceneIndex.ARENA, true, RelayContinueGame);
    }
    private void RelayStartGame()
    {
        EventManager.Instance.NewGameState();
    }
    private void RelayContinueGame()
    {
        EventManager.Instance.ContinueGameState();
    }
    public IEnumerator GetProgressLoadScene(int unloadScene, Action onComplete)
    {
        IsDoneLoading = false;
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)unloadScene));
        StartCoroutine(GetProgessUnloadScene(onComplete));
    }
    public IEnumerator GetProgessUnloadScene(Action onComplete)
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }
        loadingScreenManager.FadeOutLoadingScreen();
        onComplete?.Invoke();
        IsDoneLoading = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
