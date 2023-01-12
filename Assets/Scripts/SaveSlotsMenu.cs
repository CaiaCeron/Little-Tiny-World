using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
        saveSlots = this.GetComponentsInChildren<SaveSlots>();
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
        gameObject.SetActive(true);

        this.isUsingLoadGameButton = isUsingLoadGameButton;


        Dictionary<string, GameData> profilesGameData = GameDataManager.instance.GetAllProfilesGameData();

        foreach(SaveSlots saveSlot in saveSlots)
        {
            GameData profilaData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profilaData);
            saveSlot.SetData(profilaData);
            if (profilesGameData == null && isUsingLoadGameButton)
            {
                saveSlot.SetInteractable(false);
            }
            else
            {
                saveSlot.SetInteractable(true);
            }
        }
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

        SceneManager.LoadSceneAsync("OverWorld");

    }

}
