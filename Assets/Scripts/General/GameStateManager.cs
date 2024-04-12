using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] fishList;
    [SerializeField]
    private GameObject bigFish;
    [SerializeField]
    private OxygenBar oxyBar;
    [SerializeField]
    private ControlsManager controlManager;
    [SerializeField]
    private bool SkipTutorial;
    [SerializeField]
    private Camera mainCamArena;

    public int FishBeforeBig { get; private set; }
    public int ClearedFish { get; private set; }
    void Start()
    {
        FirstLoadState();
        FishBeforeBig = fishList.Length;
    }
    private void OnEnable()
    {
        EventManager.StartGameRelay += StartBlankSlate;
    }
    private void OnDisable()
    {
        EventManager.StartGameRelay -= StartBlankSlate;
    }

    private void FirstLoadState()
    {
        for(int i = 0; i < FishBeforeBig; i++)
        {
            fishList[i].SetActive(false);   
        }
        if (bigFish != null)
        {
            bigFish.SetActive(false);
        }
        oxyBar.OxygenActive = false;
        controlManager.PauseControls(true);
        // Set up dialogue.
    }
    public void StartBlankSlate()
    {
        ClearedFish = 0;
        // Set up to dialogue.
        fishList[ClearedFish].SetActive(true);
        // Activate oxygen tick... (DEBUG) or (Tutorial).
        if (SkipTutorial)
        {
            controlManager.PauseControls(false);
            oxyBar.OxygenActive = true;
        }
    }
    public void PauseGame()
    {
        GameManager.Instance.SwitchGameStates(GameStates.Paused);
        GameManager.Instance.AddAdditiveScene(SceneIndex.PAUSE_MENU);
    }
    public void UnpauseGame()
    {
        GameManager.Instance.SwitchGameStates(GameStates.Playing);
        GameManager.Instance.UnloadScene(SceneIndex.PAUSE_MENU);
    }
    
}
