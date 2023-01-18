using UnityEngine;
using UnityEngine.EventSystems;

public class NavigationTarget : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField]
    private bool isADenyAction = false;
    [SerializeField]
    private bool isSelectionMenu = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isADenyAction)
        {
            AudioManager.instance.PlayAudioClip("ButtonNegative");
        }
        else
        {
            AudioManager.instance.PlayAudioClip("ButtonConfirm");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelectionMenu) return;
        AudioManager.instance.PlayAudioClip("ButtonNavigation");
        
        
    }
}
