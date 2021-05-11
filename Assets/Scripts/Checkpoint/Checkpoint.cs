using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // we save a checkpointID to check in flags whether it has been triggered or not
    public string checkpointID;

    private void Start()
    {
        //self assign to dialogue
        GetComponent<Dialogue>().checkpoint = this;
    }

    public void TriggerCheckpoint()
    {
        //to prevent checkpoint from activating more than once
        if (GameManager.Instance.Blackboard.FlagManager.GetFlag(checkpointID) == false)
        {
            Debug.Log("Checkpoint " + checkpointID + " triggered");
            GameManager.Instance.Blackboard.FlagManager.SetFlag(checkpointID, true);
            GameManager.Instance.SaveGame();
        }
    }
}
