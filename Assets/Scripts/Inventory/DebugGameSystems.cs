using UnityEngine;

public class DebugGameSystems : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ChangeOutfits changeOutfit;
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

    public void ChangeAppearence(string outifit)
    {
        changeOutfit.SpriteSheetName = outifit;
    }
}
