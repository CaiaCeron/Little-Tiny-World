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

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlots>();
    }

    private void DisableMenuButtons()
    {
        foreach (SaveSlots saveSlots in saveSlots)
        {
            saveSlots.Setinteractable(false);
        
        }

        backButton.interactable = false;
    }


    public void ActiveMenu()
    {
        gameObject.SetActive(true);

        Dictionary<string, GameData> profilesGameData = GameDataManager.instance.GetAllProfilesGameData();

        foreach(SaveSlots saveSlot in saveSlots)
        {
            GameData profilaData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profilaData);
            saveSlot.SetData(profilaData);
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
        GameDataManager.instance.ChangeProfileId(saveSlot.GetProfileId());
        GameDataManager.instance.NewGame();
        SceneManager.LoadSceneAsync("OverWorld");

    }

}
