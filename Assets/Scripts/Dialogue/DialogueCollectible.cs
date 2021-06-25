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
        //if (string.IsNullOrEmpty(nodeName)) nodeName = "Collectible";
        if (dialogue) 
        { 
            runner.Add(dialogue);
            nodeName = dialogue.name;
        }
        else
        {
            nodeName = "Collectible";
        }

        if(takenFlag != "") CheckTakenStatus();
    }

    public override void StartDialogue()
    {
        SetItemName();
        if (!manual) GiveItem(null);
        else
        {
            runner.AddCommandHandler("give_item", GiveItem);
            runner.AddCommandHandler("buy_item", BuyItem);
        }
        base.StartDialogue();
    }

    /// <summary>
    /// Sets the variable used by YarnSpinner to the current item name.
    /// </summary>
    private void SetItemName()
    {
        GameManager.Instance.Blackboard.VariableStorage.SetVariable("$VARCollectibleObjName", new Value(item.name));
    }

    /// <summary>
    /// Checks if the object has been taken in this save. If it has, disable the object.
    /// </summary>
    private void CheckTakenStatus()
    {
        if (GameManager.Instance.Blackboard.FlagManager.GetFlag(takenFlag))
        {
            if (!manual) gameObject.SetActive(false);
        }
    }

    private void GiveItem(string[] parameters)
    {
        AudioController.Play("ItemPickup");
        GameManager.Instance.Blackboard.Player.playerStatus.AddItem(itemObject);
        runner.onDialogueComplete.AddListener(DisableObject);
    }

    private void BuyItem(string[] parameters)
    {
        AudioController.Play("ItemPickup");
        GameManager.Instance.Blackboard.Player.playerStatus.ModifyMoney(int.Parse(parameters[0]));
        ItemObject itemObj = new ItemObject();
        itemObj.itemID = int.Parse(parameters[1]);
        itemObj.count = int.Parse(parameters[2]);
        GameManager.Instance.Blackboard.Player.playerStatus.AddItem(itemObj);
        runner.onDialogueComplete.AddListener(DisableObject);
    }

    protected override void RemoveCommandHandlers()
    {
        runner.RemoveCommandHandler("give_item");
        runner.RemoveCommandHandler("buy_item");
        base.RemoveCommandHandlers();
    }

    private void DisableObject()
    {
        runner.onDialogueComplete.RemoveListener(DisableObject);
        if (!manual)
        {
            gameObject.SetActive(false);
        }
    }
}
