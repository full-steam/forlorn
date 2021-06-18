using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component that holds several default Yarn scripts to be loaded into the Yarn Runner.
/// </summary>
public class DialogueLoader : MonoBehaviour
{

    public YarnProgram[] dialogues;

    private void Start()
    {
        foreach (var dialogue in dialogues)
        {
            GameManager.Instance.Blackboard.DialogueRunner.Add(dialogue);
        }
    }
}
