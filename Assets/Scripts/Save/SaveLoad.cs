using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoad
{

    private const string SAVE_NAME = "data.so";

    public static void Save(SaveObject so)
    {
        #region Old Save Method
        //var save = JsonUtility.ToJson(so);
        //Debug.Log(save);
        //PlayerPrefs.SetString(keyword, save);
        //PlayerPrefs.Save();
        #endregion

        string path = Application.persistentDataPath + "/" + SAVE_NAME;
        var bf = new BinaryFormatter();
        var stream = new FileStream(path, FileMode.Create);
        bf.Serialize(stream, so);
        stream.Close();
        PlayerPrefs.SetInt("HasSaveData", 1);

        Debug.Log("Finsihed writing Save data file.");

        //Debug.LogWarning("Actual saving method is not implemented yet!\nNo data was saved.");
    }

    public static SaveObject Load()
    {
        #region Old Load Method
        //var json = PlayerPrefs.GetString(keyword, "{}");
        //return JsonUtility.FromJson<SaveObject>(json);
        #endregion

        string path = Application.persistentDataPath + "/" + SAVE_NAME;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        SaveObject so = bf.Deserialize(stream) as SaveObject;
        
        Debug.Log("Finsihed reading Save data file.");

        return so;
        //Debug.LogWarning("Actual loading method is not implemented yet!\nEmpty save object is returned");
        //return new SaveObject();
    }
}
