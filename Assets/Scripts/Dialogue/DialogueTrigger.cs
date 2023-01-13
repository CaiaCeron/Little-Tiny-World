using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink Json File")]
    [SerializeField] private TextAsset inkJson;

    private bool isInRangeTointeract;

    private void Awake()
    {
        isInRangeTointeract = false;
        visualCue.SetActive(false);   
    }

    private void Update()
    {
        ChangeVisibilityVisualCue();
    }


    private void ChangeVisibilityVisualCue()
    {
        if (isInRangeTointeract && !DialogueManager.instance.isDialogPlaying)
        {
            visualCue.SetActive(true);
            OnInteractionAction();

        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnInteractionAction()
    {
        if (Input.GetButtonDown("Interact"))
        {
            DialogueManager.instance.EnterDialogueMode(inkJson);
            Debug.Log("Interactable Button Pressed");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRangeTointeract = true;
            Debug.Log("Player is in Range for dialogue!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRangeTointeract = false;
            Debug.Log("Player is no longer in Range for dialogue!");
        }
    }
}
