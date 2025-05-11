using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] GameObject healthIndicator;
    [SerializeField] GameObject cherryCount;
    Component[] hearts;
    [SerializeField] GameObject livesCount;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hearts = healthIndicator.GetComponentsInChildren<Image>();
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TMP_Text score = cherryCount.GetComponent<TMP_Text>();
        if (score != null) { score.text = $"x {GameManager.instance.cherries}"; }
        score = livesCount.GetComponent<TMP_Text>();
        if (score != null) { score.text = $"x {GameManager.instance.lives}"; }
        if (Input.GetButtonDown("Pause"))
        {
            if (!pauseMenu.activeSelf)
            {
                Pause();
            }
            else
            {
                if (settingsMenu.activeSelf)
                {
                    CloseSettings();
                }
                Resume();
            }
        }
    }

    public void UpdateHealth(int current, int max)
    {
        for (int i = 0; i < max; i++)
        {
            Animator animator = hearts[i].GetComponent<Animator>();
            if (animator != null) { animator.SetBool("isEmpty", i >= current); }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    private void Pause()
    {
        Time.timeScale = 0.0f;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }
    
    public void CloseSettings()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

}
