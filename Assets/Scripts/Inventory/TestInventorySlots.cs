using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventorySlots : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public void AddItemToInventory(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if(result == true)
        {
            Debug.Log("Item Add");
        }
        else
        {
            Debug.Log("Inventory is full");
        }
    }

    public void RemoveItemFromInventory(int id)
    {
        bool result = inventoryManager.RemoveItem(itemsToPickup[id]);
        if(result == true)
        {
            Debug.Log("Item Removed");
        }
        else
        {
            Debug.Log("No Item in the slot");
        }
    }
}
