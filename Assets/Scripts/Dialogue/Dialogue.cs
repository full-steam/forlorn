﻿using UnityEngine;
using Yarn.Unity;

/// <summary>
/// Base component for running dialogue.
/// Only runs the set dialogue.
/// For sentence arrangement, clicks, and collectibes, use DialogueArrange, DialogueClick, and DialogueCollectible.
/// </summary>
public class Dialogue : MonoBehaviour
{
    public YarnProgram dialogue;
    [HideInInspector]
    public Checkpoint checkpoint;
    public bool triggerCheckpointDirectly;

    protected DialogueRunner runner;
    [SerializeField]protected string nodeName;

    protected virtual void Start()
    {
        runner = GameManager.Instance.Blackboard.DialogueRunner;
        nodeName = dialogue.name;
        if (dialogue) runner.Add(dialogue);
    }

    /// <summary>
    /// Starts the dialogue.
    /// </summary>
    protected virtual void StartDialogue()
    {
        runner.AddCommandHandler("trigger_checkpoint", TriggerCheckpoint);
        if (triggerCheckpointDirectly) checkpoint.TriggerCheckpoint();
        runner.StartDialogue(nodeName);
    }

    /// <summary>
    /// Triggers a checkpoint from a Yarn Program.
    /// </summary>
    /// <param name="parameters">Parameters sent from the Yarn Program. Should either be empty or have one string argument.</param>
    protected void TriggerCheckpoint(string[] parameters)
    {
        if (parameters.Length > 0) checkpoint.TriggerCheckpoint();
        else checkpoint.TriggerCheckpoint(parameters[0]);
    }
}
