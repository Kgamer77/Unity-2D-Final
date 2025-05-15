using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    //public GameObject volumeSliderPanel; // UI panel holding volume slider
    //public Slider volumeSlider;          // Slider to adjust volume
    //public Button muteButton;            // Mute button
    //public Button toggleVolumeButton;    // Button to show/hide volume UI
    //public AudioMixer audioMixer;        // Audio Mixer controlling volume

    [SerializeField] AudioClip buttonSound;

    //private float lastVolume = 0f; // Stores last volume before muting

    /*void Start()
    {
        // Initialize volume settings
        //float currentVolume;
        //audioMixer.GetFloat("MasterVolume", out currentVolume);
        //volumeSlider.value = currentVolume;
        //lastVolume = currentVolume;

        // Attach listener for slider value change
        //volumeSlider.onValueChanged.AddListener(SetVolume);

        // Hide volume slider at start
        //volumeSliderPanel.SetActive(false);

        // Attach mute/unmute function to button
        //muteButton.onClick.AddListener(ToggleMute);

        // Attach toggle volume UI function to button
        //toggleVolumeButton.onClick.AddListener(ToggleVolumeUI);
    }

    // Toggle pause state
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }

    // Show/hide the volume slider UI when button is clicked
    public void ToggleVolumeUI()
    {
        volumeSliderPanel.SetActive(!volumeSliderPanel.activeSelf);

        // Update button text based on visibility
        string buttonText = volumeSliderPanel.activeSelf ? "Hide Volume" : "Show Volume";
        toggleVolumeButton.GetComponentInChildren<Text>().text = buttonText;
    }

    // Adjust volume using the slider
    public void SetVolume(float volume)
    {
        if (!isMuted)
        {
            //audioMixer.SetFloat("MasterVolume", volume);
            lastVolume = volume; // Store last volume
        }
    }

    // Mute/unmute the audio
    public void ToggleMute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            //audioMixer.SetFloat("MasterVolume", -80f); // Fully muted
            muteButton.GetComponentInChildren<Text>().text = "Unmute"; // Update button text
        }
        else
        {
            //audioMixer.SetFloat("MasterVolume", lastVolume); // Restore last volume
            muteButton.GetComponentInChildren<Text>().text = "Mute"; // Update button text
        }
    }*/

    // Scene navigation
    public void GotoMainMenu()
    {
        AudioManager.instance.PlaySoundEffect(buttonSound, transform, 1f, 0.3f);
        SceneManager.LoadScene(1);
    }

    public void GoToGame()
    {
        AudioManager.instance.PlaySoundEffect(buttonSound, transform, 1f, 0.3f);
        SceneManager.LoadScene(4);
    }

    public void GoToCredits()
    {
        AudioManager.instance.PlaySoundEffect(buttonSound, transform, 1f, 0.3f);
        SceneManager.LoadScene(7);
    }

    public void QuitApp()
    {
        AudioManager.instance.PlaySoundEffect(buttonSound, transform, 1f, 0.3f);
        Debug.Log("Game Application has quit.");
        StartCoroutine(ExitGame());
    }

    IEnumerator ExitGame()
    {
        yield return new WaitForSeconds(buttonSound.length);
        Application.Quit();
    }
}
