using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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

    public void OnNewGameClicked()
    {
        DisableMenuButtonsUI();

        GameDataManager.instance.NewGame();

        SceneManager.LoadSceneAsync("OverWorld");

    }

    public void OnContinueGameClicked()
    {
        DisableMenuButtonsUI();

        SceneManager.LoadSceneAsync("OverWorld");
    }

    public void OnQuitGameClicked()
    {
        DisableMenuButtonsUI();

        Application.Quit();
    }
}
