using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject sceneLoader;
    [SerializeField] private int sceneIndex;

    public void GoToScene()
    {
        sceneLoader.GetComponent<SceneLoader>().LoadNextScene(sceneIndex);
    }

    public void GotoMainMenu()
    {
        sceneLoader.GetComponent<SceneLoader>().LoadNextScene(0);
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application has quit.");
    }
}
