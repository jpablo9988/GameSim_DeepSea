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
    [SerializeField]
    private RadarAnimations radar;

    public int FishBeforeBig { get; private set; }
    public int ClearedFish { get; private set; }

    public static GameStateManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogWarning("There is more than one GameStateManager. Beware!");
            Destroy(this);
        }
    }
    private void OnEnable()
    {
        FirstLoadState();
        EventManager.StartGameRelay += StartDialogue;
        EventManager.ContinueGameRelay += ContinueGame;
        EventManager.TurnBasedDone += AdvanceSaveState;
    }
    private void OnDisable()
    {
        EventManager.StartGameRelay -= StartDialogue;
        EventManager.ContinueGameRelay -= ContinueGame;
        EventManager.TurnBasedDone -= AdvanceSaveState;

    }

    private void FirstLoadState()
    {
        for(int i = 0; i < fishList.Length; i++)
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
        // Past Tutorial Dialogue.
        SaveManager.Instance.SaveData(SaveStates.POST_INTRO);
        EventManager.DialogueDone -= StartBlankSlate;
        ClearedFish = 0;
        // Set up to dialogue.
        fishList[ClearedFish].SetActive(true);
        // Activate oxygen tick... (DEBUG) or (Tutorial).
        controlManager.PauseControls(false);
        oxyBar.OxygenActive = true;
        
    }
    private void StartDialogue()
    {

        EventManager.DialogueDone += StartBlankSlate;
        StoryDirector.Instance.CallStory("Tutorial");
    }
    private void AdvanceSaveState()
    {
        fishList[ClearedFish].SetActive(false);
        SaveManager.Instance.SaveData(SaveManager.Instance.GetSaveState() + 1);
        ClearedFish++;
        if ((ClearedFish >= fishList.Length) || (SaveManager.Instance.GetSaveState() == SaveStates.VICTORY)) 
        {
            SaveManager.Instance.ResetSavedData();
            GameManager.Instance.LoadScene(SceneIndex.ARENA, SceneIndex.THE_END, true);
        }
        fishList[ClearedFish].SetActive(true);
    }
    public void ContinueGame()
    {
        SaveStates continueState = SaveManager.Instance.GetSaveState();
        if (continueState == SaveStates.NEW)
        {
            StartDialogue();
        }
        ClearedFish = ((int)continueState) - 1;
        controlManager.PauseControls(false);
        oxyBar.OxygenActive = true;
    }
    public void PauseGame()
    {
        GameManager.Instance.SwitchGameStates(GameStates.Paused);
        PauseMenuManager.Instance.ActivatePauseMenu(true);
    }
    public void UnpauseGame()
    {
        GameManager.Instance.SwitchGameStates(GameStates.Playing);
        PauseMenuManager.Instance.ActivatePauseMenu(false);
    }


}
