using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
<<<<<<< HEAD
    public void GoToGame(string Scene)
    {
        SceneManager.LoadScene(2);
    }
    public void GoToCredits(string Scene)
    {
        SceneManager.LoadScene(2);
=======
    [SerializeField] private GameObject sceneLoader;
    [SerializeField] private int sceneIndex;

    public void GoToScene()
    {
        sceneLoader.GetComponent<SceneLoader>().LoadNextScene(sceneIndex);
    }

    public void GotoMainMenu()
    {
        sceneLoader.GetComponent<SceneLoader>().LoadNextScene(0);
>>>>>>> 0ea0562a078d345f377d23d22b9b1339bfc94e4b
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Game Application has quit.");
    }
}
