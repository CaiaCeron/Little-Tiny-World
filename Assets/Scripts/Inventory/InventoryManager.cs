
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems = 9;

    [Header("Item Slot Prefab")]
    public GameObject inventoryItemPrefab;

    [Header("List slots in Inventory")]
    public InventorySlot[] slots;

    public bool AddItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlot slot = slots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.stackCount < maxStackedItems && itemInSlot.item.canStack)
            {
                itemInSlot.stackCount++;
                itemInSlot.RefreshStackCount();
                return true;
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlot slot = slots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                InsertNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    public bool RemoveItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlot slot = slots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.stackCount != 0 && itemInSlot.item.canStack)
            {
                itemInSlot.stackCount--;
                itemInSlot.RefreshStackCount();
                return true;
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlot slot = slots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                itemInSlot.stackCount--;
                return true;
            }
        }

        return false;
    }

    void InsertNewItem(Item item, InventorySlot emptySlot)
    {
        GameObject newItemGameObject = Instantiate(inventoryItemPrefab, emptySlot.transform);
        InventoryItem inventoryItem = newItemGameObject.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

   
 


    public void UpdateInventory(Item item, InventorySlot slot)
    {
        GameObject newItemGameObject = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGameObject.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);


    }

}
