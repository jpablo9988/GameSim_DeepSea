using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SINGLETON CLASS : ^)
public class GameManager : MonoBehaviour
{
    public GameStates State { get; private set; }
    public static GameManager Instance { get; private set; }
    void Awake()
    {
        State = GameStates.Playing;
        if (Instance == null) Instance = this;
        else
        {
            Debug.LogWarning("There are more than one GameManager instances in scene. Beware, traveler!");
            Destroy(this);
        }
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
}
