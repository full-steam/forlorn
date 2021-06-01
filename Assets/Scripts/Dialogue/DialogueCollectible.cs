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

    protected override void Start()
    {
        itemObject.itemID = item.id;
        itemObject.count = count;

        runner = GameManager.Instance.Blackboard.DialogueRunner;
        nodeName = "Collectible";

        CheckTakenStatus();
    }

    public override void StartDialogue()
    {
        SetItemName();
        GameManager.Instance.Blackboard.Player.playerStatus.AddItem(itemObject);
        base.StartDialogue();
    }

    /// <summary>
    /// Sets the variable used by YarnSpinner to the current item name.
    /// </summary>
    private void SetItemName()
    {
        GameManager.Instance.Blackboard.VariableStorage.SetVariable("$collectible_object_name", new Value(item.name));
    }

    /// <summary>
    /// Checks if the object has been taken in this save. If it has, disable the object.
    /// </summary>
    private void CheckTakenStatus()
    {
        if (GameManager.Instance.Blackboard.FlagManager.GetFlag(takenFlag)) gameObject.SetActive(false);
    }
}
