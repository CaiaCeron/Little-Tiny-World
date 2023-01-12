using System;
using System.IO;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using System.Collections.Generic;

public class SaveDataToFile
{
    private string dataDirPath = "";

    private string dataFileName = "";

    #region Public Methods
    public SaveDataToFile(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load(string profileId)
    {
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        GameData loadedGameData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedGameData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception ex)
            {

                UnityEngine.Debug.LogError("Something went wrong while loading the data to the file: " + fullPath + "\n" + ex);
            }

        }

        return loadedGameData;
    }

    public void Save(GameData data, string profileId) 
    {
        string fullPath = Path.Combine(dataDirPath, profileId ,dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception ex) 
        {
            UnityEngine.Debug.LogError("Something went wrong while saving the data to the file: " + fullPath + "\n" + ex);
        }
    }

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

        IEnumerable<DirectoryInfo> directoryInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();


        foreach (DirectoryInfo directoryInfo in directoryInfos)
        {
            string profileId = directoryInfo.Name;
            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
            if (!File.Exists(fullPath))
            {
                UnityEngine.Debug.LogWarning("This is not a valid directory: " + profileId);
                continue;
            }

            GameData profileData = Load(profileId);
            if (profileId != null)
            {
                profileDictionary.Add(profileId, profileData);
            }
            else
            {
                UnityEngine.Debug.LogError("Tried to load game profile but something went wrong: " + profileId);
            }

        }


        return profileDictionary;
    }


    #endregion
}