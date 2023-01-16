using System;
using System.IO;
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
        if(profileId == null)
        {
            return null;
        }

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
        if (profileId == null)
        {
            return;
        }

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

    public string GetMostUpdateProfile()
    {
        string mostRecentProfileId = null;

        Dictionary<string, GameData> profilesGameData = LoadAllProfiles();
        foreach (KeyValuePair<string, GameData> pair in profilesGameData)
        {
            string profileId = pair.Key;
            GameData gameData = pair.Value;
            if (gameData == null)
            {
                UnityEngine.Debug.LogWarning("Skip this entry because gamedata is null!!");
                continue;
            }
            if (mostRecentProfileId == null)
            {
                UnityEngine.Debug.Log("This is the first entry in mostRecentProfile variable");
                mostRecentProfileId = profileId;
            }
            else
            {
                DateTime mostRecentDateTime = DateTime.FromBinary(profilesGameData[mostRecentProfileId].lastTimeUpdate);
                DateTime newDateTime = DateTime.FromBinary(gameData.lastTimeUpdate);
                if (newDateTime > mostRecentDateTime)
                {
                    mostRecentProfileId = profileId;
                }
            }
        }

        return mostRecentProfileId;
    }

    #endregion
}