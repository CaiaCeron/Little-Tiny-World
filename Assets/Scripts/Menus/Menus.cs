using UnityEngine;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    [Header("Get First Menu Item")]
    [SerializeField] private Button firstMenuItem;


    protected virtual void OnEnable()
    {
        SetFirstItemSelected(firstMenuItem);
    }

    public void SetFirstItemSelected(Button firstItemSelected)
    {
        firstItemSelected.Select();
    }

}
