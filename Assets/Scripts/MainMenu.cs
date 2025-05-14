using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject sceneLoader;
    [SerializeField] AudioClip buttonSound;
    [SerializeField] AudioClip menuMusic;
    private AudioSource music;

    private void Start()
    {
        music = gameObject.AddComponent<AudioSource>();
        music = AudioManager.instance.GetMusicPlayer(menuMusic, 1f);
        if (music != null)
        {
            GameManager.instance.passedMusic = menuMusic;
        }
        else 
        {
            if (GameManager.instance.passedMusic != null)
            {
                menuMusic = GameManager.instance.passedMusic;
            }
        }
    }
    public void GotoMainMenu()
    {
        AudioManager.instance.PlaySoundEffect(buttonSound, transform, 1f, 0.03f);
        music.Stop();
        GameManager.instance.timeStamp = music.time;
        sceneLoader.GetComponent<SceneLoader>().LoadNextScene(1, buttonSound.length);
    }

    public void GoToGame()
    {
        AudioManager.instance.PlaySoundEffect(buttonSound, transform, 1f, 0.03f);
        sceneLoader.GetComponent<SceneLoader>().LoadNextScene(GameManager.levelSelectIndex, buttonSound.length);
    }

    public void GoToCredits()
    {
        AudioManager.instance.PlaySoundEffect(buttonSound, transform, 1f, 0.03f);
        sceneLoader.GetComponent<SceneLoader>().LoadNextScene(GameManager.creaditsIndex, buttonSound.length);
    }

    public void QuitApp()
    {
        AudioManager.instance.PlaySoundEffect(buttonSound, transform, 1f, 0.03f);
        Debug.Log("Game Application has quit.");
        StartCoroutine(ExitGame());
    }

    IEnumerator ExitGame()
    {
        yield return new WaitForSeconds(buttonSound.length);
        Application.Quit();
    }


}
