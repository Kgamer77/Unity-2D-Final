using TMPro;
using UnityEngine;

public class LevelSelectUI : MonoBehaviour
{
    [SerializeField] GameObject diamondIcon;
    [SerializeField] GameObject diamondCount;
    [SerializeField] GameObject sceneLoader;
    private AudioSource music;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Animator animator = diamondIcon.GetComponent<Animator>();
        animator.SetBool("isCollected", true);
        if (GameManager.instance.passedMusic != null)
        {
            music = gameObject.AddComponent<AudioSource>();
            music = AudioManager.instance.GetMusicPlayer(GameManager.instance.passedMusic, 1f, GameManager.instance.timeStamp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TMP_Text score = diamondCount.GetComponent<TMP_Text>();
        if (score != null) { score.text = $"x {GameManager.instance.diamonds}"; }
    }

    public GameObject GetSceneLoader()
    {
        return sceneLoader;
    }
}
