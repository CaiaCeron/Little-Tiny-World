using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public static TimeControl instance;

    public bool isGameOnPause = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PauseGame()
    {
        if (!isGameOnPause)
        {
            isGameOnPause = true;
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        if (isGameOnPause)
        {
            isGameOnPause = false;
            Time.timeScale = 1;
        }
    }
}
