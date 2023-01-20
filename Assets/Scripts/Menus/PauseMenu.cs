using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void ResumeGame()
    {
        GameManager.instance.ResumeGame();
        gameObject.SetActive(false);     
    }


    public void OnQuitGameButtonPressed()
    {
        Application.Quit();
    }
}
