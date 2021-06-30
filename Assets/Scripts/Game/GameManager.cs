using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

[RequireComponent(typeof(SaveHandler), typeof(FlagManager))]
public class GameManager : MonoBehaviour
{
    // ---Properties
    public static GameManager Instance { set; get; }
    public Blackboard Blackboard { set; get; }

    // ---Public Variables 
    public bool isPaused;
    public bool isLoading;
    public GameObject dictionaryPanel;
    public DialogueRunner dialogueRunner;
    public VariableStorage variableStorage;
    public GameObject arrangementPanel;
    public SentenceHolder sentenceHolder;
    public Button arrangementButton;
    public ArrangementOptionsHolder optionsHolder;
    public GameObject deadCanvas;
    public GameObject notEnoughMoneyPanel;
    public Button notEnoughMoneyButton;

    // ---Private Variables
    private SaveHandler saveHandler;
    private SaveObject so;
    private Action notEnoughMoneyAction;


    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else { Instance = this; DontDestroyOnLoad(gameObject); }

        isLoading = true;

        Blackboard = new Blackboard();
        saveHandler = GetComponent<SaveHandler>();

        Blackboard.DictionaryPanel = dictionaryPanel;
        Blackboard.DialogueRunner = dialogueRunner;
        Blackboard.VariableStorage = variableStorage;
        Blackboard.ArrangementPanel = arrangementPanel;
        Blackboard.SentenceHolder = sentenceHolder;
        Blackboard.ArrangementButton = arrangementButton;
        Blackboard.OptionsHolder = optionsHolder;

        if (PlayerPrefs.GetInt("HasSaveData", 0) == 0)
        {
            so = new SaveObject();
            Debug.Log("No save data found.");
        }
        else if (PlayerPrefs.GetInt("HasSaveData") == 1)
        {
            so = SaveLoad.Load();
            Debug.Log("Save data found.");
        }

        isLoading = false;
    }

    public void Pause()
    {
        isPaused = !isPaused;
    }

    public void Pause(bool pause)
    {
        isPaused = pause;
    }

    public void Dead()
    {
        deadCanvas.SetActive(true);
    }

    public void SaveGame()
    {
        so = saveHandler.GetLatestSaveData();
        SaveLoad.Save(so);

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        //so = SaveLoad.Load();     saved files are loaded at the start of the game into GameManager
        saveHandler.so = so;
        saveHandler.AssignFlags();
        //SceneManager.LoadScene(so.sceneName);
        StartCoroutine(LoadScene());
    }

    public void ChangeLevelScene(string sceneName)
    {
        var _so = new SaveObject();
        Blackboard.Player.playerStatus.GetPlayerStatus(ref _so);
        StartCoroutine(ChangeScene(sceneName, _so));
    }

    /// <summary>
    /// Enables the "Not Enough Money" feedback panel.
    /// </summary>
    /// <param name="onComplete">Action to signal to Yarn that the Command has been completed.</param>
    public void EnableNotEnoughMoneyPanel(Action onComplete)
    {
        notEnoughMoneyAction = onComplete;
        notEnoughMoneyPanel.SetActive(true);
        notEnoughMoneyButton.onClick.AddListener(OnCompleteNotEnoughMoney);
    }

    /// <summary>
    /// Triggers the Action associated with the "Not Enough Money" event.
    /// Removes itself as a listener from the button and then triggers the Action.
    /// </summary>
    private void OnCompleteNotEnoughMoney()
    {
        notEnoughMoneyButton.onClick.RemoveListener(OnCompleteNotEnoughMoney);
        notEnoughMoneyAction();
        notEnoughMoneyAction = null;
    }

    /// <summary>
    /// For debugging scene only
    /// </summary>
    public void LoadDebug()
    {
        if (PlayerPrefs.GetInt("HasSaveData") == 0) { Debug.Log("No Save data to load"); return; }
        saveHandler.so = so;
        saveHandler.AssignSaveData();
    }

    /// <summary>
    /// Load scene (from save data) asynchronously to wait until the end of first frame, letting other objects finish their (non-coroutine) Awake and Start before applying the save data.
    /// </summary>
    private IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(so.sceneName);
        asyncLoad.allowSceneActivation = true;
        while (!asyncLoad.isDone) yield return null;
        yield return new WaitForEndOfFrame();
        saveHandler.AssignSaveData();
    }

    private IEnumerator ChangeScene(string sn, SaveObject _so)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sn);
        asyncLoad.allowSceneActivation = true;
        while (!asyncLoad.isDone) yield return null;
        yield return new WaitForEndOfFrame();
        Blackboard.Player.playerStatus.AssignPlayerStatus(_so, true);
    }
}

public class Blackboard
{
    public PlayerController Player { set; get; }
    public bl_Joystick Joystick { set; get; }
    public ItemLibrary ItemLibrary { set; get; }
    public FlagManager FlagManager { set; get; }
    public DictionaryManager Dictionary { set; get; }
    public ObjectPooler ObjectPooler { set; get; }
    public DialogueRunner DialogueRunner { set; get; }
    public VariableStorage VariableStorage { set; get; }
    public GameObject DictionaryPanel { set; get; }
    public GameObject ArrangementPanel { set; get; }
    public SentenceHolder SentenceHolder { set; get; }
    public Button ArrangementButton { set; get; }
    public ArrangementOptionsHolder OptionsHolder { get; set; }
    public GameObject Camera { get; set; }
    public VolumeHandler Volume { get; set; }
    public GameObject PausePanel { get; set; }

    public Blackboard()
    {
        CreateItemLibrary();
        void CreateItemLibrary()
        {
            ItemLibrary = new ItemLibrary();
        }
    }
}
