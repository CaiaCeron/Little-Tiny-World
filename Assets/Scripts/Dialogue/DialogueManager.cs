using TMPro;
using UnityEngine;
using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; private set; }
    public bool isDialogPlaying { get; private set; }
    
    private Story currentStory;


    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;


    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Found more than one DialogueManager instance!!!");
        } 
        instance = this;
    }

    private void Start()
    {
        isDialogPlaying = false;
        dialoguePanel.SetActive(false);
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!isDialogPlaying)
        {
            return;
        }

        if (currentStory.currentChoices.Count == 0 && Input.GetButtonDown("Interact"))
        {
            ContinueStory();
        }
    }

    private void DisplayDialogueChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("List currentChoices Overflow!! " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].SetActive(false);
        }
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.25f);
        isDialogPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayDialogueChoices();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        isDialogPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    public void SelectChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }
}
