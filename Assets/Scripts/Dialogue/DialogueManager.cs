using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; private set; }
    public bool isDialogPlaying { get; private set; }
    
    private Story currentStory;

    private DialogueVariables dialogueVariables;


    [Header("Load global Variables UI")]
    [SerializeField] private TextAsset loadGlobalVariables;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;


    private void Awake()
    {
        if (instance != null)
        {
        } 
        instance = this;
        dialogueVariables = new DialogueVariables(loadGlobalVariables);

        DontDestroyOnLoad(gameObject);
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

        if (currentStory.currentChoices.Count == 0 && Input.GetButtonDown("Interact") || Input.GetMouseButton(0))
        {
            ContinueStory();
        }
    }
    private void OverlayButtonPointer()
    {
        choices[0].GetComponent<Button>().Select();
    }

    private void DisplayDialogueChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        if (currentChoices.Count > choices.Length)
        {
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

    

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        isDialogPlaying = true;
        dialoguePanel.SetActive(true);
        dialogueVariables.StartListening(currentStory);
        OverlayButtonPointer();
        ContinueStory();

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

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.5f);
        dialogueVariables.StopListening(currentStory);
        isDialogPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    public void SelectChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    public Ink.Runtime.Object GetVariable(string variableName)
    {
        Ink.Runtime.Object result = null;
        dialogueVariables.variables.TryGetValue(variableName, out result);
        if (result == null)
        {

        }

        return result;
    }


    public void SetVariable()
    {
        currentStory.EvaluateFunction("OpenBuyScreen", "");
    }

   
}
