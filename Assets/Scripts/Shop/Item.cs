using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data")]
public class Item : ScriptableObject
{
	public Sprite icon;
	public int id;
	public int price;
	public ItemType itemType;

	public bool canStack = true;

	public enum ItemType
	{
		outfit,
		food
	}
}
