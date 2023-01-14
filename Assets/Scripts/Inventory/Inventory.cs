using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static event Action<List<InventoryItem>> OnInventoryChange;

    public List<InventoryItem> inventoryItems = new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();


    private void OnEnable()
    {
        Apple.OnAppleColected += AddToDictionary;
        Gem.OnGemsCollected += AddToDictionary;
    }

    private void OnDisable()
    {
        Apple.OnAppleColected -= AddToDictionary;
        Gem.OnGemsCollected -= AddToDictionary;

    }


    public void AddToDictionary(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToItemStack();
            OnInventoryChange?.Invoke(inventoryItems);
            Debug.Log($"{item.itemData.itemName} total stack is now: {item.itemStackSize}");
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            inventoryItems.Add(newItem);
            itemDictionary.Add(itemData, newItem);
            OnInventoryChange?.Invoke(inventoryItems);
            Debug.Log($"Uau you have collected your first {itemData.itemName}");
        }

    }

    public void RemoveFromDictionary(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromItemStack();
            if (item.itemStackSize == 0)
            {
                inventoryItems.Remove(item);
                itemDictionary.Remove(itemData);
            }
            OnInventoryChange?.Invoke(inventoryItems);
        }
    }
}
