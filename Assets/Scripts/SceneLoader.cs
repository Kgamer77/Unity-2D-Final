using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject transition;
    [SerializeField] private float transitionTime = 1.0f;

    public void LoadNextScene(int sceneIndex)
    {
        StartCoroutine(LoadScene(sceneIndex));
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        transition.GetComponent<Animator>().SetTrigger("start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneIndex);
    }

}
