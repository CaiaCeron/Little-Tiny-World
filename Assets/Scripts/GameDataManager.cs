using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour
{
    [Header("File Storage config")]
    [SerializeField] private string fileName;

    [Header("Debbugin Mode")]
    [SerializeField] private bool initializeDataOnEditor = false;

    private SaveDataToFile dataToFile;

    public static GameDataManager instance { get; private set; }
    
    private GameData gameData;

    private List<IDataPersistence> gameDataPersistenceObjects;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found and old instance of GameDataManager. Newest one wil be Destroyed.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);

        this.dataToFile = new SaveDataToFile(Application.persistentDataPath, fileName);
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
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }



    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataToFile.Load();

        if (this.gameData == null && initializeDataOnEditor)
        {
            NewGame();
        }


        if (this.gameData == null)
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

        if (this.gameData == null)
        {
            Debug.LogWarning("No save game data was found. Please select 'New Game' option on Main Menu");
            return;
        }

        foreach (IDataPersistence dataPersistence in gameDataPersistenceObjects)
        {
            dataPersistence.SaveGameData(ref gameData);
        }

        dataToFile.Save(gameData);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.gameDataPersistenceObjects = FindAllGameDataPersistence();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene)
    {

        SaveGame();

    }

    public bool HasGameData()
    {
        return this.gameData != null;
    }
}


