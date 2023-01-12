using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotsMenu : Menus
{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenu mainMenu;

    private SaveSlots[] saveSlots;

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlots>();
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

}
