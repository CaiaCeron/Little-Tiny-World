using System;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
    public Inventory shopInventory;
    public ShopSystem shopSystem;

    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink Json File")]
    [SerializeField] private TextAsset inkJson;

    [Header("Shopkeeper Properties")]
    public string playerTag;
    public string shopName;
    public bool canSellTo = true;
    public bool finiteMoney = true;
    public bool finiteItems = true;
    private bool isInRangeToInteract = false;


    private void Update()
    {
        ChangeVisibilityVisualCue();
        DialogueChoiceSelection();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            isInRangeToInteract = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            isInRangeToInteract = false;
        }
    }
    private void OnInteractionAction()
    {
        if (Input.GetButtonDown("Interact") || Input.GetButtonDown("Submit"))
        {
            DialogueManager.instance.EnterDialogueMode(inkJson);
        }
    }

    private void ChangeVisibilityVisualCue()
    {
        if (isInRangeToInteract && !DialogueManager.instance.isDialogPlaying)
        {
            visualCue.SetActive(true);
            OnInteractionAction();
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void DialogueChoiceSelection()
    {
        string dialogChoice = ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariable("choiceMade")).value;
        switch (dialogChoice)
        {
            case "":
                break;

            case "0":
                shopSystem.OpenShop(this);
                DialogueManager.instance.SetVariable();
                break;   
                
        }
    }
}
