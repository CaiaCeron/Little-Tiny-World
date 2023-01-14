using UnityEngine;

public class Apple : MonoBehaviour, ICollectible
{
    public delegate void HandleApplesCollected(ItemData itemData);
    public static event HandleApplesCollected OnAppleColected;
    public ItemData appleData;

    public void Collect()
    {
        OnAppleColected?.Invoke(appleData);
        Destroy(gameObject);
        Debug.Log("You Collected an apple!!");
    }

}
