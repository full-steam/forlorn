using UnityEngine.EventSystems;
using Yarn;

/// <summary>
/// Component to run dialogue when the object is clicked.
/// </summary>
public class DialogueClick : Dialogue, IPointerClickHandler
{
    public string objName;

    protected override void Start()
    {
        runner = GameManager.Instance.Blackboard.DialogueRunner;
        nodeName = "Click";

        if (string.IsNullOrEmpty(objName)) objName = gameObject.name; 
    }

    protected override void StartDialogue()
    {
        SetObjectName();
        base.StartDialogue();
    }

    /// <summary>
    /// Sets the variable used by YarnSpinner to the current object name.
    /// </summary>
    private void SetObjectName()
    {
        GameManager.Instance.Blackboard.VariableStorage.SetVariable("$click_object_name", new Value(objName));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartDialogue();
    }
}
