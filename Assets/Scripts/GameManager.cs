using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int diamonds = 0;
    public int lives = 3;
    public int cherries = 0;
    public HashSet<int> collectedDiamonds = new HashSet<int>();
    public const int gameVictoryIndex = 2;
    public const int gameOverIndex = 3;
    public const int creaditsIndex = 4;
    public const int levelSelectIndex = 5;
    public AudioClip passedMusic = null;
    public float timeStamp = 0f;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
