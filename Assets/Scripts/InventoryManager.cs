using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems = 9;
    private int selectedSlot = -1;


    [Header("Item Slot Prefab")]
    public GameObject inventoryItemPrefab;

    [Header("List slots in Inventory")]
    public InventorySlot[] slots;


    private void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0) slots[selectedSlot].DeselectSlot();
        
        slots[selectedSlot].DeselectSlot();
        slots[newValue].SelectSlot();
        selectedSlot = newValue;
    }

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

    void InsertNewItem(Item item, InventorySlot emptySlot)
    {
        GameObject newItemGameObject = Instantiate(inventoryItemPrefab, emptySlot.transform);
        InventoryItem inventoryItem = newItemGameObject.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

    public Item GetSelectedItem()
    {
        InventorySlot slot = slots[2];

        return null;
    }

}
