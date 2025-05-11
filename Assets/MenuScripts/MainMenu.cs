using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool isPaused = false;

    public void GotoMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene(GameManager.levelSelectIndex);
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene(GameManager.creaditsIndex);
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Game Application has quit.");
    }

    // Pause and resume functionality
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }
}
