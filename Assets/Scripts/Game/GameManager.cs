using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SaveHandler))]
public class GameManager : MonoBehaviour
{
    // ---Properties
    public static GameManager Instance { set; get; }
    public Blackboard Blackboard { set; get; }

    // ---Public Variables 
    public bool isPaused;

    // ---Private Variables
    private SaveHandler saveHandler;
    private SaveObject so;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else { Instance = this; DontDestroyOnLoad(gameObject); }

        Blackboard = new Blackboard();
        saveHandler = GetComponent<SaveHandler>();
        so = new SaveObject();
    }

    public void Pause()
    {
        isPaused = !isPaused;
    }

    public void Pause(bool pause)
    {
        isPaused = pause;
    }

    public void SaveGame()
    {
        so = saveHandler.GetLatestSaveData();
        SaveLoad.Save(so);

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        so = SaveLoad.Load();
        saveHandler.so = so;
        SceneManager.LoadScene(so.sceneName);
        StartCoroutine(LoadScene());
    }

    /// <summary>
    /// Load scene asynchronously to wait until the end of first frame, letting other objects finish their (non-coroutine) Awake and Start before applying the save data.
    /// </summary>
    private IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(so.sceneName);
        while (!asyncLoad.isDone) yield return null;
        yield return new WaitForEndOfFrame();
        saveHandler.AssignSaveData();
    }
}

public class Blackboard 
{
    //public PlayerController Player { set; get; }
    //public bl_Joystick Joystick { set; get; }
    //public ItemLibrary ItemLibrary { set; get; }
    //public FlagManager Flag { set; get; }
    //public DictionaryManager Dictionary { set; get; }

    public Blackboard()
    {
        CreateItemLibrary();
        void CreateItemLibrary()
        {
            //ItemLibrary = new ItemLibrary();
        }
    }
}
