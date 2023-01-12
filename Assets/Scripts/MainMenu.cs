using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : Menus
{
    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;

    [Header("Main Menu")]
    [SerializeField] private Button btnNewGame;
    [SerializeField] private Button btnContinueGame;
    [SerializeField] private Button btnQuitGame;

    private void DisableMenuButtonsUI()
    {
        btnNewGame.interactable = false;
        btnContinueGame.interactable = false;
        btnQuitGame.interactable = false;
    }

    private void Start()
    {
        if (GameDataManager.instance.HasGameData())
        {
            btnContinueGame.interactable = true;
        } 
    }

    public void OnNewGameButtonPressed()
    {
        saveSlotsMenu.ActiveMenu();
        this.DeactivateMenu();

    }

    

    public void OnContinueGameButtonPressed()
    {
        DisableMenuButtonsUI();

        SceneManager.LoadSceneAsync("OverWorld");
    }

    public void OnQuitGameButtonPressed()
    {
        DisableMenuButtonsUI();

        Application.Quit();
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
