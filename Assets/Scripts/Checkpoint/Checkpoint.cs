using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // we save a checkpointID to check in flags whether it has been triggered or not
    public string checkpointId;

    private void Start()
    {
        // self assign to dialogue
        GetComponent<Dialogue>().checkpoint = this;
    }

    /// <summary>
    /// Triggers the checkpoint associated with this checkpoint object.
    /// </summary>
    public void TriggerCheckpoint()
    {
        TriggerCheckpoint(checkpointId);
    }

    /// <summary>
    /// Triggers a specific checkpoint by checkpoint ID.
    /// </summary>
    /// <param name="checkpointId">Flag or checkpoint ID of the checkpoing to be triggered.</param>
    public void TriggerCheckpoint(string checkpointId)
    {
        // to prevent checkpoint from activating more than once
        if (GameManager.Instance.Blackboard.FlagManager.GetFlag(checkpointId) == false)
        {
            Debug.Log("Checkpoint " + checkpointId + " triggered");
            GameManager.Instance.Blackboard.FlagManager.SetFlag(checkpointId, true);
            GameManager.Instance.SaveGame();
        }
    }
}
