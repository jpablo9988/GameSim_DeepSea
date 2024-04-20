using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDirector : MonoBehaviour
{
    [SerializeField]
    TextAsset story;
    public static StoryDirector Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            Debug.LogWarning("There's already a Story Director. Beware");
        }
    }
    public void CallStory(string caseName)
    {
        DialogueManager.Instance.EnterDialogue(story, caseName);
    }
}
