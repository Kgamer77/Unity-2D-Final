using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] GameObject healthIndicator;
    [SerializeField] GameObject cherryCount;
    Component[] hearts;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hearts = healthIndicator.GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        TMP_Text score = cherryCount.GetComponent<TMP_Text>();
        if (score != null) { score.text = $"x {GameManager.instance.cherries}"; }
    }

    public void UpdateHealth(int current, int max)
    {
        for (int i = 0; i < max; i++)
        {
            Animator animator = hearts[i].GetComponent<Animator>();
            if (animator != null) { animator.SetBool("isEmpty", i >= current); }
        }
    }
}
