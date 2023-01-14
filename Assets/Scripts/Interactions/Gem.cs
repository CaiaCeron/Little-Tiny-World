using UnityEngine;

public class Gem : MonoBehaviour, ICollectible
{
    public delegate void HandleGemsCollected(ItemData itemData);
    public static event HandleGemsCollected OnGemsCollected;
    public ItemData gemsData;

    public void Collect()
    {
        OnGemsCollected?.Invoke(gemsData);
        Destroy(gameObject);
        Debug.Log("You Collected a gem!!");
    }
}
