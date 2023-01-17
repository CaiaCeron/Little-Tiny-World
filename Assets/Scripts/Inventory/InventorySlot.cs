using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventorySlot : MonoBehaviour,IDropHandler
{
    public Image image;
    public Color selectedColor;
    public Color notSelectedColor;


    public void Awake()
    {
        DeselectSlot();
    }

    public void SelectSlot()
    {
        image.color = selectedColor;
    }

    public void DeselectSlot()
    {
        image.color = notSelectedColor;
    }


    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount != 0) return;

        GameObject drop = eventData.pointerDrag;
        InventoryItem dragItem = drop.GetComponent<InventoryItem>();
        dragItem.parentWhileDragging = transform;

    }
    
}