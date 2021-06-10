using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles as a mediator between actual game data with GameManager
/// </summary>
public class SaveHandler : MonoBehaviour
{
    public SaveObject so;
    public PlayerController player;
    public FlagManager flag;

    public bool inDebugScene;
    public SaveDataTesting sdt;

    private void Start()
    {
        flag = GetComponent<FlagManager>();     //flag persists with GameManager, so only need to get the ref once
        so = new SaveObject();
    }

    /// <summary>
    /// SaveHandler check the latest Player reference in Blackboard.
    /// </summary>
    public void GetPlayerRef()
    {
        player = GameManager.Instance.Blackboard.Player;
    }

    public void AssignSaveData()
    {
        GetPlayerRef();
        if (inDebugScene)
        {
            sdt.saveObject = so;
            sdt.UpdateUI();
        }
        else
        {
            player.playerStatus.AssignPlayerStatus(so);
        }
    }

    public void AssignFlags()
    {
        flag.InitFlags(so);
    }

    public SaveObject GetLatestSaveData()
    {
        GetPlayerRef();
        if (inDebugScene)
        {
            sdt.GetPlayerStatus(ref so);
        }
        else
        {
            player.playerStatus.GetPlayerStatus(ref so);
        }

        flag.SaveFlags(ref so);

        so.sceneName = SceneManager.GetActiveScene().name;

        return so;
    }
}
