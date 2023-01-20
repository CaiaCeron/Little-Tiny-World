using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public Image itemImage;
    public TextMeshProUGUI stackText;

    [HideInInspector]
    public Item item;
    [HideInInspector]
    public int stackCount = 1;
    [HideInInspector]
    public Transform parentWhileDragging;



    void Start()
    {  
        InitializeItem(item);
    }

    public void InitializeItem(Item newItem)
    {
        item = newItem;
        itemImage.sprite = newItem.icon;
    }

    public void RefreshStackCount()
    {
        stackText.text = stackCount.ToString();
        bool hideText = stackCount > 1;
        stackText.gameObject.SetActive(hideText);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentWhileDragging = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        itemImage.raycastTarget = false;  
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentWhileDragging);
        itemImage.raycastTarget = true;
    }
}
