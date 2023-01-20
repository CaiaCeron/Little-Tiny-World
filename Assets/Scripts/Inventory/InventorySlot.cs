using UnityEngine;
using UnityEngine.EventSystems;


public class InventorySlot : MonoBehaviour,IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount != 0) return;

        GameObject drop = eventData.pointerDrag;
        InventoryItem dragItem = drop.GetComponent<InventoryItem>();
        dragItem.parentWhileDragging = transform;
    }   
}