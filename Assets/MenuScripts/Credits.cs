using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public float scrollSpeed = 50f;

    void Update()
    {
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
    }

    // Scene navigation methods
    public void GotoMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene(4);
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene(7);
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Game Application has quit.");
    }
}
