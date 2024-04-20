using System.Collections;
using System.Collections.Generic;
using TMPro;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

/*                  singleton class - dialogue manager.
 *           deals with dialogue loading and showcase in-game.
 */
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    /*                                      Tags
     *      In INK, tags will decide what name, portrait and dialogue speed to play.    */
    private const string T_CHAR = "character";
    private const string T_DIALG_SPEED = "speed";
    private const string T_PAUSE_DIALG = "pause";
    private const string T_PORTRAIT_STATE = "state";

    private List<string> localStory;
    private Story currentStory;
    private string v_CurrentSentence = "";
    private int v_CurrMaxVisibleCharacters = 0;


    Dictionary<(int, string), string> v_DialogueEventDict;
    private IEnumerator c_DialogueCoroutine;

    // ... Serializable Variables ... //
    [SerializeField]
    private Animator a_portrait;
    [SerializeField]
    private bool v_CanFastfowardText = true;
    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI u_textDisplay;
    [SerializeField]
    private TextMeshProUGUI u_nameDisplay;
    [SerializeField]
    private GameObject u_dialoguePanel;
    [SerializeField]
    private List<Button> u_buttonGameObjects;

    [SerializeField]
    private AudioSource talkingSound;
    [SerializeField]
    private bool repeatTalkingSoundPerLetter = false;

    // ... Properties ... //
    public bool IsTextAdvancable { get; private set; }
    public bool IsDialogueRunning { get; private set; }
    public bool P_CanFastfowardText { get { return v_CanFastfowardText; } private set { v_CanFastfowardText = value; } }
    public bool P_IsCoroutineRunning { get; private set; }


    void Awake()
    {
        IsDialogueRunning = false;
        P_IsCoroutineRunning = false;
        v_DialogueEventDict = new Dictionary<(int, string), string>();
        if (Instance == null) Instance = this;
        else
        {
            Destroy(this);
            Debug.LogWarning("Duplicate instance of DialogueManager has been found");
        }

        u_dialoguePanel.SetActive(false);
        IsTextAdvancable = false;
    }
    /*                      SetInkVariables override for character.
                Takes into account existing lies from characters, and if a character forgot information.
                Changes character dialogue depending on character mood towards player.
     */

    public void EnterDialogue(TextAsset inkFile, string pathString)
    {
        SetUpInkDialogue(inkFile, pathString);
        ContinueStory();
    }
    private void SetUpInkDialogue(TextAsset inkFile, string pathString)
    {
        u_dialoguePanel.SetActive(true);
        currentStory = new Story(inkFile.text);
        currentStory.ChoosePathString(pathString);
        IsDialogueRunning = true;    
    }
    public void ExitDialogue()
    {
        u_dialoguePanel.SetActive(false);
        IsDialogueRunning = false;
        EventManager.Instance.DialogueIsDone();
    }
    private void HandleTags(List<string> currentTags)
    {
        v_DialogueEventDict.Clear();

        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');

            if (splitTag.Length > 2)
            {
                Debug.LogWarning("Tag length is not supported and it will not be parsed");

            }

            string tagKey = splitTag[0];

            switch (tagKey)
            {

                case T_PORTRAIT_STATE:
                    if (a_portrait != null)
                    {
                        if (splitTag[1].Contains(','))
                        {
                            AddToDictionary(splitTag);

                        }
                        else
                        {
                            int stateID = Animator.StringToHash(splitTag[1]);
                            if (this.a_portrait.HasState(0, stateID))
                                this.a_portrait.Play(splitTag[1]);
                            else
                            {
                                //Debug.Log("Using this tag: " + splitTag[1] + ", We couldn't find the portrait.");
                                this.a_portrait.Play("default");
                            }
                        }
                    }
                    break;

                case T_CHAR:
                    u_nameDisplay.text = splitTag[1];
                    break;
                default:
                    if (tagKey.Equals(T_DIALG_SPEED) || tagKey.Equals(T_PAUSE_DIALG))
                    {
                        AddToDictionary(splitTag);
                    }
                    else
                    {
                        Debug.LogWarning("Tag is not recognized and it will be ignored");
                    }
                    break;
            }
        }
    }
    public void ContinueStory()
    {
        
        if (currentStory.canContinue)
        {

           if (u_textDisplay != null)
           {

                v_CurrentSentence = currentStory.Continue();
                v_CurrMaxVisibleCharacters = v_CurrentSentence.Length;
                c_DialogueCoroutine = WriteDialogue(v_CurrentSentence);
                StartCoroutine(c_DialogueCoroutine);

           }
        }
        else
        {
            ExitDialogue();
        }
        
    }
    private void AddToDictionary(string[] splitTag)
    {
        /* splitInfo[0] containts the data (speed of dialogue), and splitInfo[1] contains
                     in which word is it executed. */
        string[] splitInfo = splitTag[1].Split(',');
        try
        {
            int newValue;
            newValue = int.Parse(splitInfo[1]);
            (int, string) auxTuple = (newValue, splitTag[0]);
            v_DialogueEventDict.Add(auxTuple, splitInfo[0]);
        }
        catch (FormatException e)
        {
            Debug.LogWarning("Tag " + splitTag[0] + " not in the correct format: " + e.Message + " \n Skipping Tag.");
        }
    }
    private int wordCounter = 0;
    private float currDialogueSpeed = 0.2f;
    private bool isFirstLetter = true;

    IEnumerator WriteDialogue(String sentence)
    {
        bool isPaused = false;
        int letterCounter = 0;
        IsTextAdvancable = false;
        P_IsCoroutineRunning = true;
        isFirstLetter = true;
        wordCounter = 0;
        u_textDisplay.text = sentence;
        u_textDisplay.maxVisibleCharacters = 0;
        HandleTags(currentStory.currentTags);
        char[] arraySentence = sentence.ToCharArray();
        if (!repeatTalkingSoundPerLetter)
            talkingSound.Play();
        foreach (char letter in arraySentence)
        {
            v_CanFastfowardText = true;
            if (isPaused)
            {
                isPaused = false;
                a_portrait.SetBool("Talk", true);
            }
            float pauseTimer = 0;
            if (Char.IsWhiteSpace(letter) && (letterCounter + 1 < arraySentence.Length))
            {
                if (!Char.IsWhiteSpace(arraySentence[letterCounter + 1]))
                {
                    isFirstLetter = true;
                    wordCounter++;
                }
            }
            if (isFirstLetter)
            {
                foreach ((int, string) tuple in v_DialogueEventDict.Keys)
                {
                    if (tuple.Item1 == wordCounter)
                    {
                        switch (tuple.Item2)
                        {

                            case T_PORTRAIT_STATE:
                                if (a_portrait != null)
                                {
                                    int stateID = Animator.StringToHash(v_DialogueEventDict[tuple]);
                                    if (this.a_portrait.HasState(0, stateID))
                                        this.a_portrait.Play(v_DialogueEventDict[tuple]);
                                    else
                                    {
                                        Debug.LogWarning("Using this tag: " + v_DialogueEventDict[tuple] + ", We couldn't find the portrait.");
                                        this.a_portrait.Play("default");
                                    }
                                }

                                break;
                            case T_DIALG_SPEED:
                                try
                                {
                                    currDialogueSpeed = float.Parse(v_DialogueEventDict[tuple], CultureInfo.InvariantCulture);
                                }
                                catch (FormatException e)
                                {
                                    Debug.LogWarning("Dialogue Speed not in the correct format. " + e.Message + " \n Skipping Tag.");
                                }
                                break;
                            case T_PAUSE_DIALG:
                                try
                                {
                                    pauseTimer = float.Parse(v_DialogueEventDict[tuple], CultureInfo.InvariantCulture);
                                    isPaused = true;
                                }
                                catch (FormatException e)
                                {
                                    Debug.LogWarning("Pause not in the correct format. " + e.Message + " \n Skipping Tag.");
                                }
                                break;

                        }
                    }
                }
                isFirstLetter = false;
            }
            u_textDisplay.maxVisibleCharacters++;
            yield return new WaitForSeconds(currDialogueSpeed + pauseTimer);
            letterCounter++;
            if (repeatTalkingSoundPerLetter)
                talkingSound.Play();
        }
        talkingSound.Stop();
        FinishSentenceConditions();
    }
    public bool GetCanAdvanceText()
    {
        return this.v_CanFastfowardText;
    }
    private void FinishSentenceConditions()
    {
        v_CanFastfowardText = false;
        P_IsCoroutineRunning = false;
        IsTextAdvancable = true;
    }
    public void SkipDialogueCoroutine()
    {
        if (P_IsCoroutineRunning)
        {
            FinishSentenceConditions();
            StopCoroutine(c_DialogueCoroutine);
            u_textDisplay.maxVisibleCharacters = v_CurrMaxVisibleCharacters;
        }
    }
}
