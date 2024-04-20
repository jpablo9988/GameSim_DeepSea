using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*          -- SINGLETON --
 *  Simple Save Manager.
 *          As we're working with simple save states, PlayerPrefs will do.
 */
public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private SaveStates currentState;

    public static SaveManager Instance;

    public static readonly string PLAYER_STATE = "PlayerState", VOLUME_PREFS = "VolumePreferences";
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There are more than one SaveManagers. Beware!");
            Destroy(this);
        }
    }
    private void OnEnable()
    {
        RetrieveState();
        Settings.Volume = GetVolumeSettings();
    }
    public void ResetSavedData()
    {
        PlayerPrefs.DeleteKey(PLAYER_STATE);
    }
    private void RetrieveState()
    {
        if (PlayerPrefs.HasKey(PLAYER_STATE))
        {
            currentState = (SaveStates)PlayerPrefs.GetInt(PLAYER_STATE);
        }
        else
        {
            currentState = SaveStates.NEW;
        }
    }
    public void SaveData(SaveStates state)
    {
        PlayerPrefs.SetInt(PLAYER_STATE, (int)state);
        currentState = state;
    }
    public SaveStates GetSaveState()
    {
        return currentState;
    }
    public void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat(VOLUME_PREFS, Settings.Volume);
    }
    public float GetVolumeSettings()
    {
        if (PlayerPrefs.HasKey(VOLUME_PREFS))
        {
            return PlayerPrefs.GetFloat(VOLUME_PREFS);
        }
        else
        {
            return 0.85f;
        }
    }
}
