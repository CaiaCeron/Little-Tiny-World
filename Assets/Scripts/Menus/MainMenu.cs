using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : Menus
{
    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;

    [Header("Main Menu")]
    [SerializeField] private Button btnNewGame;
    [SerializeField] private Button btnContinueGame;
    [SerializeField] private Button btnLoadGame;
    [SerializeField] private Button btnQuitGame;


    private void DisableMenuButtonsUI()
    {
        btnNewGame.interactable = false;
        btnContinueGame.interactable = false;
        btnQuitGame.interactable = false;
    }

    private void Start()
    {
        if (!GameDataManager.instance.HasGameData())
        {
            btnContinueGame.interactable = false;
            btnLoadGame.interactable = false;
        } 
    }

    public void OnNewGameButtonPressed()
    {
        saveSlotsMenu.ActiveMenu(false);
        DeactivateMenu();

    }

    

    public void OnContinueGameButtonPressed()
    {
        DisableMenuButtonsUI();
 
        GameDataManager.instance.SaveGame();

        SceneManager.LoadSceneAsync("OverWorld");
    }


    public void OnLoadGameButtonPressed() 
    {

        saveSlotsMenu.ActiveMenu(true);
        DeactivateMenu();
    }

    public void OnQuitGameButtonPressed()
    {
        DisableMenuButtonsUI();


        Application.Quit();
    }

    public void ActivateMenu()
    {
        gameObject.SetActive(true);
    }

    public void DeactivateMenu()
    {
        gameObject.SetActive(false);
    }
}
