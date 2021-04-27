using UnityEngine;

public static class SaveLoad
{
    public static void Save(SaveObject so /*, string keyword = "saveData"*/)
    {
        var save = JsonUtility.ToJson(so);
        Debug.Log(save);

        //PlayerPrefs.SetString(keyword, save);
        //PlayerPrefs.Save();

        Debug.LogWarning("Actual saving method is not implemented yet!\nNo data was saved.");
    }

    public static SaveObject Load(/*string keyword = "saveData"*/)
    {
        Debug.LogWarning("Actual loading method is not implemented yet!\nEmpty save object is returned");
        return new SaveObject();

        //var json = PlayerPrefs.GetString(keyword, "{}");
        //return JsonUtility.FromJson<SaveObject>(json);
    }
}
