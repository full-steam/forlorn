using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component to handle Dialogue that triggers when player enter/collide its proximity
/// </summary>
public class DialogueTrigger : Dialogue
{
    public bool disableAfterTrigger = true;

    protected override void Start()
    {
        base.Start();
        if (GetComponent<Collider2D>() == null) Debug.LogError("No collider found in " + gameObject.name + "!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartDialogue();
            if (disableAfterTrigger) gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartDialogue();
            if (disableAfterTrigger) gameObject.SetActive(false);
        }
    }
}
