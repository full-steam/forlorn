using System;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    public event Action<string> FlagTriggered;

    private Dictionary<string, bool> flags;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public bool GetFlag(string flag)
    {
        return flags[flag];
    }

    public void SetFlag(string flag, bool trigger)
    {
        flags[flag] = trigger;
        FlagTriggered(flag);
    }

    public void InitFlags(Dictionary<string, bool> dict)
    {
        // Slight error in Class Diagram sketch
        // Missing parameter name: `Dictionary<string, bool> *`
    }

    // TODO: Determine whether this is required
    public Dictionary<string, bool> GetFlagsAll()
    {
        return flags;
    }
}
