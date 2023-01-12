using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour
{
    [Header("Debbugin Mode")]
    [SerializeField] private bool initializeDataOnEditor = false;
    [SerializeField] private bool disableDataPersistence = false;
    [SerializeField] private bool overrideSelectedProfileId = false;
    [SerializeField] private string testProfile = "TEST";


    [Header("File Storage config")]
    [SerializeField] private string fileName;
    

    public static GameDataManager instance { get; private set; }
    private List<IDataPersistence> gameDataPersistenceObjects;
    private SaveDataToFile dataToFile;
    private GameData gameData;
    private string selectedProfileId = "";


    #region Private Methods

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found and old instance of GameDataManager. Newest one wil be Destroyed.");
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);

        if (disableDataPersistence) 
        {
            Debug.LogWarning("Data Persistence is Disable!!!");
        }

        dataToFile = new SaveDataToFile(Application.persistentDataPath, fileName);
        selectedProfileId = dataToFile.GetMostUpdateProfile();

        if (overrideSelectedProfileId)
        {
            selectedProfileId = testProfile;
            Debug.LogWarning("Override Profile is Enable!!!");
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataPersistence> FindAllGameDataPersistence()
    {
        IEnumerable<IDataPersistence> gameDataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(gameDataPersistenceObjects);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion


    #region Public Methods

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    { 
        if (disableDataPersistence)
        {
            return;
        }

        gameData = dataToFile.Load(selectedProfileId);

        if (gameData == null && initializeDataOnEditor)
        {
            NewGame();
        }


        if (gameData == null)
        {
            Debug.LogWarning("No game data was found. Please select 'New Game' option on Main Menu.");
            return;
        }

        foreach(IDataPersistence dataPersistence in gameDataPersistenceObjects)
        {
            dataPersistence.LoadGameData(gameData);
        }
        
    }

    public void SaveGame()
    {
        if (disableDataPersistence)
        {
            return;
        }

        if (gameData == null)
        {
            Debug.LogWarning("No save game data was found. Please select 'New Game' option on Main Menu");
            return;
        }

        foreach (IDataPersistence dataPersistence in gameDataPersistenceObjects)
        {
            dataPersistence.SaveGameData(gameData);
        }

        gameData.lastTimeUpdate = System.DateTime.Now.ToBinary();

        dataToFile.Save(gameData, selectedProfileId);

    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.gameDataPersistenceObjects = FindAllGameDataPersistence();
        LoadGame();
    }

    public bool HasGameData()
    {
        return this.gameData != null;
    }
    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataToFile.LoadAllProfiles();
    }

    public void ChangeProfileId(string newProfileID)
    {
        selectedProfileId= newProfileID;
    }


    #endregion
}


