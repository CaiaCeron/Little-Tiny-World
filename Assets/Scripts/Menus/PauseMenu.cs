using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void ResumeGame()
    {
        TimeControl.instance.ResumeGame();
        gameObject.SetActive(false);     
    }


    public void OnQuitGameButtonPressed()
    {
        Application.Quit();
    }
}
