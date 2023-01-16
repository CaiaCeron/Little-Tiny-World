using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab;

    private List<InventorySlot> slots = new List<InventorySlot>(21);


    private void OnEnable()
    {
        LittleTyneWorld.Inventory.OnInventoryChange += DrawInventory;
    }

    private void OnDisable()
    {
        LittleTyneWorld.Inventory.OnInventoryChange -= DrawInventory;
    }

    void ResetInventory()
    {
        foreach (Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }

        slots = new List<InventorySlot>(21);
    }

    void DrawInventory(List<InventoryItem> inventory)
    {
        ResetInventory();
        for (int i = 0; i < slots.Capacity; i++)
        {
            CreateinventorySlots();
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            slots[i].DrawItemInSlot(inventory[i]);
        }

    }

    void CreateinventorySlots()
    {
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform, false);

        InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
        newSlotComponent.ClearItemInSlot();

        slots.Add(newSlotComponent);
    }
}
