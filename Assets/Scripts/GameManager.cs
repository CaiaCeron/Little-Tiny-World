using UnityEngine;

public class GameManager : MonoBehaviour, IDataPersistence
{
    public static GameManager instance { get; private set; }

    //References
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private ShopManager shopManager;
    //References

    //Game States
    public bool isGamePaused = false;
    public bool isShopping = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    public int GetPlayerMoney()
    {
        return player.GetMoney();
    }

    public int GetShopkeeperMoney(int money)
    {
        Debug.Log(money);
        return money;
    }


    public void PauseGame()
    {
        if (!isGamePaused)
        {
            isGamePaused = true;
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        if (isGamePaused)
        {
            isGamePaused = false;
            Time.timeScale = 1;
        }
    }

    public void LoadGameData(GameData data)
    {

    }

    public void SaveGameData(GameData data)
    {
        
    }
}
