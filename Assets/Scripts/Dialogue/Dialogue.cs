using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn;
//using Yarn.Unity;         uncomment if uses DialogueRunner

public abstract class Dialogue : MonoBehaviour
{
    //Yarn's ScriptableObject
    public YarnProgram dialogue;


    //My first idea is to let each dialogue component to get reference to the DialogueRunner (or any script that runs Yarn)
    //protected DialogueRunner runner;

    //Holds the name of the main node of the YarnProgram
    protected string nodeName;

    //Add any other attribute if needed (e.g for flag, etc.)

    public abstract void StartDialogue();
    //Add any other behaviour if needed (e.g for flag, etc.)
}
