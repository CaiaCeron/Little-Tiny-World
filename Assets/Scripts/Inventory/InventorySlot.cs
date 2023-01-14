using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemStackText;


    public void ClearItemInSlot()
    {
        icon.enabled = false;
        itemStackText.enabled = false;
    }

    public void ShowItemInSlot()
    {
        icon.enabled = true;
        itemStackText.enabled = true;
    }

    public void DrawItemInSlot(InventoryItem item)
    {
        if (item == null)
        {
            ClearItemInSlot();
            return;
        }

        ShowItemInSlot();
        icon.sprite = item.itemData.itemSprite;
        itemStackText.text = item.itemStackSize.ToString();
    }
}