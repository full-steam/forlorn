using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// ---------------------------------------------!
// Does this need a collider?
// [RequireComponent(typeof(Collider2D))]
// ---------------------------------------------!
public class DialogueClick : Dialogue, IPointerClickHandler
{
    //Dialogue attribute can/should be omitted here. 
    //AFAIK we just need to load "Nodes" which is part of YarnProgram (where 1 YarnProgram can hold more than 1 node), then by loading to the YarnProgram to the Yarn runner (i.e DialogueRunner.cs/our own custom class), we can access all those nodes from everywhere as long as we have the node name (string). Then we just need to modify variables to change the text according to the current component.

    public new string name;

    private void Start()
    {
        //nodeName = (STATIC_CLICK_NODE_NAME)
    }

    public override void StartDialogue()
    {
        //SetObjectName();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartDialogue();
    }

    private void SetObjectName()
    {
        //change the variable that holds the object name to be shown on Dialogue (that will be read from the node)
    }
}
