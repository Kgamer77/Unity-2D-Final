using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject transition;
    [SerializeField] private float transitionTime = 1.0f;

    public void LoadNextScene(int sceneIndex, float waitTime = 0f)
    {
        StartCoroutine(LoadScene(sceneIndex, waitTime));
    }

    IEnumerator LoadScene(int sceneIndex, float waitTime)
    {
        transition.GetComponent<Animator>().SetTrigger("start");

        yield return new WaitForSeconds(transitionTime + waitTime);

        SceneManager.LoadScene(sceneIndex);
    }

}
