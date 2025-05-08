using UnityEngine;

public class Collectible : MonoBehaviour
{

    public enum CollectType
    {
        Cherry,
        Diamond,
        Heart,
        Flag,
        Goal
    }
        
    [SerializeField] private CollectType type;
    private bool isCollected = false;
    [SerializeField] private int diamondUID = -1;

    private void Start()
    {
        foreach (int uid in GameManager.instance.collectedDiamonds)
        {
            if (diamondUID == uid)
            {
                isCollected = true;
                break;
            }
        }
    }

    public CollectType GetCollectType()
    {
        return type;
    }

    public bool IsCollected()
    {
        return isCollected;
    }

    public int GetDiamondUID()
    {
        return diamondUID;
    }
}
