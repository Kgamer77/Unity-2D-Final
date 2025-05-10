using TMPro;
using UnityEngine;

public class LevelSelectUI : MonoBehaviour
{
    [SerializeField] GameObject diamondIcon;
    [SerializeField] GameObject diamondCount;
    [SerializeField] GameObject sceneLoader;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Animator animator = diamondIcon.GetComponent<Animator>();
        animator.SetBool("isCollected", true);
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
