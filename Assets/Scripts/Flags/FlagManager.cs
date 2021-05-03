using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages flags for in-game events.
/// </summary>
public class FlagManager : MonoBehaviour
{
    public event Action<string> FlagTriggered;

    private Dictionary<string, bool> flags;

    // Start is called before the first frame update
    private void Start()
    {
        if (GameManager.Instance.Blackboard.FlagManager != null && GameManager.Instance.Blackboard.FlagManager != this) Destroy(this);
        else
        {
            GameManager.Instance.Blackboard.FlagManager = this;
        }
    }

    /// <summary>
    /// Returns the value of a flag.
    /// </summary>
    /// <param name="flag">Name of the requested value's flag.</param>
    /// <returns>Value of the requested flag.</returns>
    public bool GetFlag(string flag)
    {
        return flags[flag];
    }

    /// <summary>
    /// Sets the value of a flag.
    /// </summary>
    /// <param name="flag">Name of the flag.</param>
    /// <param name="value">Value to be set.</param>
    public void SetFlag(string flag, bool value)
    {
        flags[flag] = value;
        FlagTriggered(flag);
    }

    // TODO: Integrate with Save module
    public void InitFlags()
    {
        
    }

    /// <summary>
    /// Sets all flags to false. See VariableStorage.
    /// </summary>
    public void ResetFlags()
    {
        foreach (var key in flags.Keys)
        {
            flags[key] = false;
        }
    }

    /// <summary>
    /// Saves the flags to a SaveObject with separate lists for the keys and values.
    /// </summary>
    /// <param name="saveObj">SaveObject to save the flags to.</param>
    /// <returns>Modified SaveObject.</returns>
    public SaveObject SaveFlags(SaveObject saveObj)
    {
        saveObj.keys = new List<string>(flags.Keys).ToArray();
        saveObj.values = new List<bool>(flags.Values).ToArray();
        return saveObj;
    }
}
