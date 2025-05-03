using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int diamonds = 0;
    public int lives = 3;
    public int cherries = 0;

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
