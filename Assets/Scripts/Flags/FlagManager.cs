using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{

    private Dictionary<string, bool> flags;
    public Action<string> OnFlagTriggered; //should this be a property?

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetFlag(string flag)
    {

        return new bool();
    }

    public void SetFlag(string flag, bool trigger)
    {
        // Slight error in Class Diagram sketch
        // Reversed name placement in: `..., bool : trigger`
    }

    public void InitFlags(Dictionary<string, bool> dict)
    {
        // Slight error in Class Diagram sketch
        // Missing parameter name: `Dictionary<string, bool> *`
    }

    public Dictionary<string, bool> GetFlagsAll()
    {

        //return flags;
        return new Dictionary<string, bool>();
    }
}
