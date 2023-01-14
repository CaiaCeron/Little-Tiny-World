using System;

[Serializable]
public class InventoryItem
{
    public ItemData itemData;

    public int itemStackSize;


    public InventoryItem(ItemData itemData)
    {
        this.itemData = itemData;
        AddToItemStack();
    }


    public void AddToItemStack()
    {
        itemStackSize++;
    }

    public void RemoveFromItemStack()
    {
        itemStackSize--;
    }
    
}
