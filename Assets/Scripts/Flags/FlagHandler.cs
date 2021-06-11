using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component responsible for interacting with the Flag Manager.
/// </summary>
public class FlagHandler : MonoBehaviour
{
    public List<FlagEvent> flagEvents;

    // Start is called before the first frame update
    private void Awake()
    {
        GameManager.Instance.Blackboard.FlagManager.FlagTriggered += OnFlagTriggered;
    }

    /// <summary>
    /// Triggers an event and sets the flag value to true.
    /// </summary>
    /// <param name="flag">Name of the triggered flag.</param>
    public void TriggerEvent(string flag)
    {
        GameManager.Instance.Blackboard.FlagManager.SetFlag(flag, true);
    }

    /// <summary>
    /// Listener to be run when a flag is triggered.
    /// </summary>
    /// <param name="flag">Name of the triggered flag.</param>
    private void OnFlagTriggered(string flag)
    {
        foreach (FlagEvent flagEvent in flagEvents)
        {
            if (flagEvent.flag == flag)
            {
                flagEvent.ExecuteEvent();
            }
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.Blackboard.FlagManager.FlagTriggered -= OnFlagTriggered;
    }
}
