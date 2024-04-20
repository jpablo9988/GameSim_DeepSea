using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueControls : MonoBehaviour
{
    
    void Update()
    {
        if (DialogueManager.Instance.IsDialogueRunning)
        {
            if (Input.anyKeyDown)
            {
                if (DialogueManager.Instance.P_CanFastfowardText)
                {
                    DialogueManager.Instance.SkipDialogueCoroutine();
                }
                else if (DialogueManager.Instance.IsTextAdvancable)
                {
                    DialogueManager.Instance.ContinueStory();
                }
            }
        }
    }
}
