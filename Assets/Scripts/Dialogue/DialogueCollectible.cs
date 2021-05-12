using Yarn;

public class DialogueCollectible : Dialogue
{
    //Dialogue attribute can/should be omitted here. 
    //AFAIK we just need to load "Nodes" which is part of YarnProgram (where 1 YarnProgram can hold more than 1 node), then by loading to the YarnProgram to the Yarn runner (i.e DialogueRunner.cs/our own custom class), we can access all those nodes from everywhere as long as we have the node name (string). Then we just need to modify variables to change the text according to the current component.

    public Item item;
    public int count;
    public string takenFlag;
    private int itemID;

    protected override void Start()
    {
        //TODO: Get Item ID after Item module implemented
        //itemID = (get from ItemLibrary)

        runner = GameManager.Instance.Blackboard.DialogueRunner;
        nodeName = "Collectible";

        CheckTakenStatus();
    }

    public override void StartDialogue()
    {
        SetItemName();
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
