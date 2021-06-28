using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestRunner : MonoBehaviour
{
    public float monologueDelay = 4.0f;
    public List<GameObject> disabledUIElements;

    private Dialogue dialogue;

    private void Awake()
    {
        dialogue = GetComponent<Dialogue>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        GameManager.Instance.Blackboard.Player.playerMovement.ToggleMovement(false);
        foreach (GameObject element in disabledUIElements)
        {
            element.SetActive(false);
        }
        GameManager.Instance.Blackboard.FlagManager.CheckFlags();
        if (!GameManager.Instance.Blackboard.FlagManager.GetFlag("HasMetMovement")) StartCoroutine(StartMonologue());
    }

    private IEnumerator StartMonologue()
    {
        yield return new WaitForSeconds(monologueDelay);

        dialogue.StartDialogue();
    }
}
