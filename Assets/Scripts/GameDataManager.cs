using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GameDataManager : MonoBehaviour
{
    [Header("File Storage config")]
    [SerializeField] private string fileName;

    private SaveDataToFile dataToFile;

    public static GameDataManager instance { get; private set; }
    
    private GameData gameData;

    private List<IDataPersistence> gameDataPersistenceObjects;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Scene have more than one GameDataManager!!");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataToFile = new SaveDataToFile(Application.persistentDataPath, fileName);
        this.gameDataPersistenceObjects = FindAllGameDataPersistence();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataToFile.Load();

        if (this.gameData == null)
        {
            Debug.Log("initialize gameData with default values!");
            NewGame();
        }

        foreach(IDataPersistence dataPersistence in gameDataPersistenceObjects)
        {
            dataPersistence.LoadGameData(gameData);
        }
        
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistence in gameDataPersistenceObjects)
        {
            dataPersistence.SaveGameData(ref gameData);
        }

        dataToFile.Save(gameData);
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
}


