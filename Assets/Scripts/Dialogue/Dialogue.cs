using System;
using UnityEngine;
using Yarn.Unity;

/// <summary>
/// Base component for running dialogue.
/// Only runs the set dialogue.
/// For sentence arrangement, clicks, and collectibes, use DialogueArrange, DialogueClick, and DialogueCollectible.
/// </summary>
public class Dialogue : MonoBehaviour
{
    [Tooltip("Giving a node name manually will takes priority to be ran than the set Dialogue")]
    public string nodeName;
    public YarnProgram dialogue;

    protected DialogueRunner runner;

    protected virtual void Start()
    {
        runner = GameManager.Instance.Blackboard.DialogueRunner;
        if (string.IsNullOrEmpty(nodeName)) nodeName = dialogue.name;
        if (dialogue)
        {
            if (!runner.NodeExists(dialogue.name)) runner.Add(dialogue);
        }
    }

    /// <summary>
    // Starts the dialogue.
    /// </summary>
    public virtual void StartDialogue()
    {
        GameManager.Instance.Blackboard.Player.playerMovement.ToggleMovement(false);
        runner.AddCommandHandler("trigger_checkpoint", TriggerCheckpoint);
        runner.AddCommandHandler("teleport", Teleport);
        runner.AddCommandHandler("fade_out_and_in", FadeOutAndIn);
        runner.onDialogueComplete.AddListener(RemoveCommandHandlers);
        runner.StartDialogue(nodeName);
    }

    /// <summary>
    /// Triggers a flag and saves the game.
    /// </summary>
    /// <param name="parameters">Parameters sent from the Yarn Program. Should have one string argument: the flag triggered.</param>
    protected void TriggerCheckpoint(string[] parameters)
    {
        string flag = parameters[0];

        // prevents checkpoint from activating more than once
        if (GameManager.Instance.Blackboard.FlagManager.GetFlag(flag) == false)
        {
            Debug.Log("[Checkpoint]: Checkpoint " + flag + " triggered.");
            GameManager.Instance.Blackboard.FlagManager.SetFlag(flag, true);
            GameManager.Instance.SaveGame();
        }
    }

    protected virtual void RemoveCommandHandlers()
    {
        runner.RemoveCommandHandler("trigger_checkpoint");
        runner.RemoveCommandHandler("teleport");
        runner.RemoveCommandHandler("fade_out_and_in");
        runner.onDialogueComplete.RemoveListener(RemoveCommandHandlers);
    }

    protected void Teleport(string[] parameters)
    {
        Vector3 pos = new Vector3(float.Parse(parameters[0]), float.Parse(parameters[1]), float.Parse(parameters[2]));
        GameManager.Instance.Blackboard.Player.transform.SetPositionAndRotation(pos, Quaternion.identity);
    }

    protected void FadeOutAndIn(string[] parameters, Action onComplete)
    {
        GameManager.Instance.Blackboard.Camera.GetComponent<CameraFadeManager>().FadeOutAndIn(onComplete);
    }
}
