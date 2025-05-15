using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class LevelPanel : MonoBehaviour
{
    [SerializeField] private GameObject thumbnail;
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private GameObject levelLabel;
    [SerializeField] private GameObject diamondCountIcon;
    [SerializeField] private int levelIndex = 0;
    [SerializeField] private ushort unlockAmount = 0;
    [SerializeField] private int levelUid = 0;
    [SerializeField] private string levelName = "Level";
    [SerializeField] GameObject diamonds;
    [SerializeField] int[] diamondUIDs;
    private Animator[] icons;
    [SerializeField] AudioClip levelMusic;
    [SerializeField] AudioClip buttonSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        diamondCountIcon.GetComponent<Animator>().SetBool("isCollected", true);
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

        if (GameManager.instance.unlockedLevels.Contains(levelUid))
        {
            diamondCountIcon.SetActive(false);
            Animator animator = thumbnail.GetComponent<Animator>();
            animator.SetTrigger("unlocked");
            animator = lockIcon.GetComponent<Animator>();
            animator.SetTrigger("unlocked");
        }
        else
        {
            diamondCountIcon.SetActive(true);
            levelLabel.GetComponent<TMP_Text>().text = $" x {unlockAmount}";
        }
    }

    public void OnButtonClicked()
    {
        if (!GameManager.instance.unlockedLevels.Contains(levelUid))
        {
            if (GameManager.instance.diamonds >= unlockAmount)
            {
                GameManager.instance.unlockedLevels.Add(levelUid);
                // play the animations
                StartCoroutine(UnlockLevel());
                
            }
        }
        else
        {
            AudioManager.instance.PlaySoundEffect(buttonSound, transform, 1f, 0.03f);
            if (levelMusic != null) { GameManager.instance.passedMusic = levelMusic; }
            GetComponentInParent<LevelSelectUI>().GetSceneLoader().GetComponent<SceneLoader>().LoadNextScene(levelIndex, buttonSound.length);
        }
    }

    IEnumerator UnlockLevel()
    {
        diamondCountIcon.SetActive(false);
        levelLabel.GetComponent<TMP_Text>().text = levelName;
        Animator animator = lockIcon.GetComponent<Animator>();
        animator.SetTrigger("unlock");
        animator = thumbnail.GetComponent<Animator>();
        animator.SetTrigger("unlock");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        animator.SetTrigger("unlocked");
        animator = lockIcon.GetComponent<Animator>();
        animator.SetTrigger("unlocked");
    }
}
