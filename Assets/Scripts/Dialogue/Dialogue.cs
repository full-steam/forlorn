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
    public Checkpoint checkpoint;
    public bool triggerCheckpointDirectly;

    protected DialogueRunner runner;

    protected virtual void Start()
    {
        runner = GameManager.Instance.Blackboard.DialogueRunner;
        if (string.IsNullOrEmpty(nodeName)) nodeName = dialogue.name;
        if (dialogue) runner.Add(dialogue);
    }

    /// <summary>
    /// Starts the dialogue.
    /// </summary>
    public virtual void StartDialogue()
    {
        GameManager.Instance.Blackboard.Player.playerMovement.ToggleMovement(false);
        runner.AddCommandHandler("trigger_checkpoint", TriggerCheckpoint);
        runner.AddCommandHandler("teleport", Teleport);
        if (triggerCheckpointDirectly) checkpoint.TriggerCheckpoint();
        runner.onDialogueComplete.AddListener(RemoveCommandHandlers);
        runner.StartDialogue(nodeName);
    }

    /// <summary>
    /// Triggers a checkpoint from a Yarn Program.
    /// </summary>
    /// <param name="parameters">Parameters sent from the Yarn Program. Should either be empty or have one string argument.</param>
    protected void TriggerCheckpoint(string[] parameters)
    {
        if (parameters.Length <= 0) this.checkpoint.TriggerCheckpoint();
        else checkpoint.TriggerCheckpoint(parameters[0]);
    }

    protected virtual void RemoveCommandHandlers()
    {
        runner.RemoveCommandHandler("trigger_checkpoint");
        runner.RemoveCommandHandler("teleport");
        runner.onDialogueComplete.RemoveListener(RemoveCommandHandlers);
    }

    protected void Teleport(string[] parameters)
    {
        Vector3 pos = new Vector3(float.Parse(parameters[0]), float.Parse(parameters[1]), float.Parse(parameters[2]));
        GameManager.Instance.Blackboard.Player.transform.SetPositionAndRotation(pos, Quaternion.identity);
    }
}
