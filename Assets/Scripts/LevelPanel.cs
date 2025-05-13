using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPanel : MonoBehaviour
{
    [SerializeField] private int levelIndex = 0;
    [SerializeField] GameObject diamonds;
    [SerializeField] int[] diamondUIDs;
    private Animator[] icons;
    [SerializeField] AudioClip levelMusic;
    [SerializeField] AudioClip buttonSound;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        icons = diamonds.GetComponentsInChildren<Animator>();
        for (int i = 0; i<diamondUIDs.Length; i++)
        {
            foreach (int uid in GameManager.instance.collectedDiamonds)
            {
                if (diamondUIDs[i] == uid)
                {
                    icons[i].SetBool("isCollected", true);
                    break;
                }
            }
        }
    }

    public void OnButtonClicked()
    {
        AudioManager.instance.PlaySoundEffect(buttonSound, transform, 1f, 0.03f);
        if (levelMusic != null) { GameManager.instance.passedMusic = levelMusic; }
        GetComponentInParent<LevelSelectUI>().GetSceneLoader().GetComponent<SceneLoader>().LoadNextScene(levelIndex, buttonSound.length);
    }

}
