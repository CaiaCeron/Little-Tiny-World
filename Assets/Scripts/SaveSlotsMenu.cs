using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class SaveSlotsMenu : Menus
{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenu mainMenu;

    [Header("Menu Buttons")]
    [SerializeField] private Button backButton;

    private SaveSlots[] saveSlots;

    [SerializeField] private bool isUsingLoadGameButton = false;

    private void Awake()
    {
        saveSlots = GetComponentsInChildren<SaveSlots>();
    }

    private void DisableMenuButtons()
    {
        foreach (SaveSlots saveSlots in saveSlots)
        {
            saveSlots.SetInteractable(false);
        
        }

        backButton.interactable = false;
    }


    public void ActiveMenu(bool isUsingLoadGameButton)
    {   
        GameObject firstSelected = backButton.gameObject;

        gameObject.SetActive(true);

        this.isUsingLoadGameButton = isUsingLoadGameButton;

        Dictionary<string, GameData> profilesGameData = GameDataManager.instance.GetAllProfilesGameData();

        foreach(SaveSlots saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
            if (profileData == null && isUsingLoadGameButton)
            {
                saveSlot.SetInteractable(false);
            }
            else
            {
                saveSlot.SetInteractable(true);
                if (firstSelected.Equals(backButton.gameObject))
                {
                    firstSelected = saveSlot.gameObject;
                }

            }
        }
        Button firstButtonSelected = firstSelected.GetComponent<Button>();
        SetFirstItemSelected(firstButtonSelected);
    }


    public void DeactivateMenu()
    {
        gameObject.SetActive(false);
    }


    public void OnBackButtonPressed()
    {
        mainMenu.ActivateMenu();
        DeactivateMenu();
    }

    public void OnSaveSlotButtonPressed(SaveSlots saveSlot)
    {
        DisableMenuButtons();

        GameDataManager.instance.ChangeProfileId(saveSlot.GetProfileId());
        if (!isUsingLoadGameButton)
        {
            GameDataManager.instance.NewGame();
        }

        GameDataManager.instance.SaveGame();

        SceneManager.LoadSceneAsync("OverWorld");

    }

}
