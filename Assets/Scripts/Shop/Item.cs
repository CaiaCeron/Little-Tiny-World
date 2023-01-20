using UnityEngine;

[CreateAssetMenu(fileName = "Data")]
public class Item : ScriptableObject
{
	public int id;
	public int price;
	public Sprite icon;
	public ItemType itemType;
	public bool canStack = true;
    public enum ItemType
    {
        outfit,
        food
    }
}
