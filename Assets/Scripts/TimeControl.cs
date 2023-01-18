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
        if (!isGameOnPause && Time.timeScale == 1 )
        {
            Time.timeScale = 0;
            isGameOnPause = true;
        }
    }

    public void ResumeGame()
    {
        if (isGameOnPause && Time.timeScale == 0)
        {
            isGameOnPause = false;
            Time.timeScale = 1;
        }
    }
}
