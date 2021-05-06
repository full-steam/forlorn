using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChoice : Dialogue
{

    // We probably don't need this, as dialogues with only Choices can be managed from YarnProgram directly.
    // So we probably need to make the base class' method virtual and use that for generic convos OR we make another derived class for generic convos

    public override void StartDialogue()
    {

    }
}
