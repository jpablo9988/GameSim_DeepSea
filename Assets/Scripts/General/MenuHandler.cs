
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    // Pause Menu Functionality
     public void ContinueGame()
    {
        GameStateManager.Instance.UnpauseGame();
    }
     public void Settings()
    {
        PauseMenuManager.Instance.ActivateSettingsTab(true);
    }
     public void BackToMainMenu()
    {
        //Go to main menu.
        // Save last checkpoint. 
        GameStateManager.Instance.UnpauseGame();
        GameManager.Instance.LoadScene(SceneIndex.ARENA, SceneIndex.TITLE_SCREEN);
    }
   
}
