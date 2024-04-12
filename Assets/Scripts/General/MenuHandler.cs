
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    // Main Menu
    public void start_game()
    {
        Debug.Log("The game is started!!");
    }
    public void load_game()
    {
        Debug.Log("Your game is loading!!");
    }
    public void gallery()
    {
        Debug.Log("You are in the gallery!!");
    }
    public void exit()
    {
        Application.Quit();
    }

    // Pause Menu
     public void continue_game()
    {
        Debug.Log("The game has continue!!");
    }
     public void game_setting()
    {
        Debug.Log("The game setting is open!!");
    }
     public void main_menu()
    {
        Debug.Log("You are back at the main menu!!");
    }
   
}
