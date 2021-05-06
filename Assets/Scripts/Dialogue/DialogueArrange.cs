using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueArrange : Dialogue
{
    public override void StartDialogue()
    {
        
    }

    public void EndDialogue()
    {

    }

    private void ArrangeSentence()
    {
        //I actually forgot what I meant in sketch (so take this with a grain of salt), 許してください :<
        //
        //To integrate Arrange in Yarn dialogue, we need to give use a Command that will call a method that is a CommandHandler, so we add this method to the CommandHandler when we START the dialogue from this component, then remove it at the END
        //ps: 'Blocking' as in (temporarily) stopping Yarn Dialogue, should be
    }
}
