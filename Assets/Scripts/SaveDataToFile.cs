using System;
using System.IO;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class SaveDataToFile
{
    private string dataDirPath = "";

    private string dataFileName = "";


    public SaveDataToFile(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
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

    public void Save(GameData data) 
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

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
}