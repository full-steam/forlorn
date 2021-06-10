using UnityEngine;
using Yarn;

/// <summary>
/// Component to run dialogue when an item is collected.
/// </summary>
public class DialogueCollectible : Dialogue
{
    public Item item;
    public int count;
    public string takenFlag;
    private ItemObject itemObject;
    public bool manual;

    protected override void Start()
    {
        itemObject = new ItemObject();
        itemObject.itemID = item.id;
        itemObject.count = count;

        runner = GameManager.Instance.Blackboard.DialogueRunner;
        if (string.IsNullOrEmpty(nodeName)) nodeName = "Collectible";
        if (dialogue) runner.Add(dialogue);

        CheckTakenStatus();
    }

    public override void StartDialogue()
    {
        SetItemName();
        if (!manual) GiveItem(null);
        else runner.AddCommandHandler("give_item", GiveItem);
        base.StartDialogue();
    }

    /// <summary>
    /// Sets the variable used by YarnSpinner to the current item name.
    /// </summary>
    private void SetItemName()
    {
        Debug.Log(GameManager.Instance);
        Debug.Log(GameManager.Instance.Blackboard);
        Debug.Log(GameManager.Instance.Blackboard.VariableStorage);
        Debug.Log(new Value(1));
        GameManager.Instance.Blackboard.VariableStorage.SetVariable("$VARCollectibleObjName", new Value(item.name));
    }

    /// <summary>
    /// Checks if the object has been taken in this save. If it has, disable the object.
    /// </summary>
    private void CheckTakenStatus()
    {
        if (GameManager.Instance.Blackboard.FlagManager.GetFlag(takenFlag)) gameObject.SetActive(false);
    }

    private void GiveItem(string[] parameters)
    {
        GameManager.Instance.Blackboard.Player.playerStatus.AddItem(itemObject);
    }

    protected override void RemoveCommandHandlers()
    {
        runner.RemoveCommandHandler("give_item");
        base.RemoveCommandHandlers();
    }
}
