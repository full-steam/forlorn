using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class SaveDataTesting : MonoBehaviour
{

    [Tooltip("Don't change this")]
    public TMP_Text[] keys;
    [Tooltip("Don't change this")]
    public TMP_Text[] values;

    public SaveObject saveObject;

    private GameManager gm;
    private Blackboard bb;

    private void Start()
    {
        gm = GameManager.Instance;
        bb = gm.Blackboard;
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            if (saveObject.keys.Count != saveObject.values.Count)
            {
                Debug.LogWarning("Keys and Values don't have the same number of component, changes were not saved to FlagManager");
            }
            else
            {
                StartCoroutine(UpdateFlag());
            }
        }
    }

    IEnumerator UpdateFlag()
    {
        yield return new WaitForEndOfFrame();
        bb.FlagManager.InitFlags(saveObject);
        UpdateUI();
        Debug.Log("Keys and Values applied to FlagManager");
    }

    public void GetPlayerStatus(ref SaveObject stat)
    {
        stat.health = saveObject.health;
        stat.hunger = saveObject.hunger;
        stat.distanceSum = saveObject.distanceSum;
        stat.steps = saveObject.steps;
        stat.starving = saveObject.starving;
        stat.posInScene = saveObject.posInScene;
        stat.itemList = saveObject.itemList;
    }

    public void AssignPlayerStatus(SaveObject stat)
    {
        saveObject.health = stat.health;
        saveObject.hunger = stat.hunger;
        saveObject.distanceSum = stat.distanceSum;
        saveObject.steps = stat.steps;
        saveObject.starving = stat.starving;
        saveObject.posInScene = stat.posInScene;
        saveObject.itemList = stat.itemList;
    }

    public void UpdateUI()
    {
        ClearTextFields();
        // check the value of the key in the actual dictionary to make sure it was saved correctly
        for (int i = 0; i < saveObject.keys.Count; i++)
        {
            keys[i].text = saveObject.keys[i];
            values[i].text = bb.FlagManager.GetFlag(saveObject.keys[i]).ToString();
        }
    }

    void ClearTextFields()
    {
        foreach (var TMP in keys)
        {
            TMP.text = "";
        }
        foreach (var TMP in values)
        {
           TMP.text = "";
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DeleteSaveData()
    {
        PlayerPrefs.SetInt("HasSaveData", 0);
        string path = Application.persistentDataPath + "/data.so";
        File.Delete(path);
        Debug.Log("File save deleted");
    }
}
