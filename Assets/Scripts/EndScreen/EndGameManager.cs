using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void BackToMenu()
    {
        GameManager.Instance.LoadScene(SceneIndex.THE_END, SceneIndex.TITLE_SCREEN, true);
    }
}
