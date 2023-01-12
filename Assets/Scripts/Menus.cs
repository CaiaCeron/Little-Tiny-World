using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menus : MonoBehaviour
{
    [Header("Get First Menu Item")]
    [SerializeField] private GameObject firstMenuItem;


    protected virtual void OnEnable()
    {
        StartCoroutine(SetFirstItemSelected(firstMenuItem));
    }

    public IEnumerator SetFirstItemSelected(GameObject firstItemSelected)
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(firstItemSelected);
    }

}
