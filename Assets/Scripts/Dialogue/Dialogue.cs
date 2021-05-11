using UnityEngine;
using Yarn.Unity;

/// <summary>
/// Base component for running dialogue.
/// Only runs the set dialogue.
/// For sentence arrangement, clicks, and collectibes, use DialogueArrange, DialogueClick, and DialogueCollectible.
/// </summary>
public class Dialogue : MonoBehaviour
{
    public YarnProgram dialogue;
    public bool triggerCheckpointDirectly;

    public Checkpoint checkpoint;

    protected DialogueRunner runner;
    protected string nodeName;

    protected virtual void Start()
    {
        runner = GameManager.Instance.Blackboard.DialogueRunner;
        nodeName = dialogue.name;
        if (dialogue) runner.Add(dialogue);
    }

    /// <summary>
    /// Starts the dialogue.
    /// </summary>
    public virtual void StartDialogue()
    {
        if (triggerCheckpointDirectly) checkpoint.TriggerCheckpoint();
        runner.StartDialogue(nodeName);
    }
}
