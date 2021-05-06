using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCollectible : Dialogue
{
    //Dialogue attribute can/should be omitted here. 
    //AFAIK we just need to load "Nodes" which is part of YarnProgram (where 1 YarnProgram can hold more than 1 node), then by loading to the YarnProgram to the Yarn runner (i.e DialogueRunner.cs/our own custom class), we can access all those nodes from everywhere as long as we have the node name (string). Then we just need to modify variables to change the text according to the current component.

    public Item item;
    public int count;
    private int itemID;

    private void Start()
    {
        //TODO: Get Item ID after Item module implemented
        //itemID = (get from ItemLibrary)

        //nodeName = (STATIC_COLLECTIBLE_NODE_NAME)
    }

    public override void StartDialogue()
    {
        //SetItemName();
    }

    private void SetItemName()
    {
        //change the variable that holds the item name to be shown on Dialogue (that will be read from the node)
    }

    private void CheckTakenStatus()
    {
        //I'm not sure how to save these collectibles status, read and write to flags?
        
        //Disable script/object if already taken.
    }
}
