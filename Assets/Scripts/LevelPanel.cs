using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPanel : MonoBehaviour
{
    [SerializeField] private int levelIndex = 0;
    [SerializeField] GameObject diamonds;
    [SerializeField] int[] diamondUIDs;
    private Animator[] icons;


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
        Debug.Log("Clicked");
        //SceneManager.LoadScene(levelIndex);
    }

}
