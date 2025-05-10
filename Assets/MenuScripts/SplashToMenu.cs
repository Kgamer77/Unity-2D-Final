using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashToMenu : MonoBehaviour
{


    void Start()
    {
        StartCoroutine(TakeToMenu());
    }

    IEnumerator TakeToMenu()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(1);
    }

}