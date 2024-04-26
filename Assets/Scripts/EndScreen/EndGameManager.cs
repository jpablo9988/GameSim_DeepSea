using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void BackToMenu()
    {

        GameManager.Instance.LoadScene(SceneIndex.THE_END, SceneIndex.TITLE_SCREEN);
    }
}
