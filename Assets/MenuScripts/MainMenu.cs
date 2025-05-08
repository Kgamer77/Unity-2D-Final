using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToGame(string Scene)
    {
        SceneManager.LoadScene(2);
    }
    public void GoToCredits(string Scene)
    {
        SceneManager.LoadScene(2);
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Game Application has quit.");
    }
}
