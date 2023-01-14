using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable: MonoBehaviour
{
    public bool isInRangeTointeract = false;
    public string buttonName;
    public UnityEvent interactAction;


    private void Update()
    {
        if (isInRangeTointeract)
        {
            if (Input.GetButtonDown(buttonName))
            {
                interactAction.Invoke();
                
            }
        }
    }


    internal void OnInteractionAction(string buttonName)
    {
        if (Input.GetButtonDown(buttonName))
        {
            interactAction.Invoke();
            Debug.Log("Interactable Button Pressed");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRangeTointeract = true;
            Debug.Log("Player is in Range!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRangeTointeract = false;
            Debug.Log("Player is no longer in Range!");
        }    
    }
}
   
