using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GotoMainMenu()
    {
        SceneManager.LoadScene(1);

    }
    public void GoToGame()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene(2);
    }
    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Game Application has quit.");
    }
}
