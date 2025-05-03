using UnityEngine;

public class Collectible : MonoBehaviour
{

    public enum CollectType
    {
        Cherry,
        Diamond,
        Heart
    }
        
    [SerializeField] private CollectType type;
    private bool isCollected = false;

    public CollectType GetCollectType()
    {
        return type;
    }

    public bool IsCollected()
    {
        return isCollected;
    }
}
