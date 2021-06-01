using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public Button InteractButton { set; get; }
    [SerializeField] private GameObject toInteract;

    public void Interact()
    {
        toInteract.GetComponent<Dialogue>().StartDialogue();
    }

    public void Interact(GameObject _toInteract)
    {
        _toInteract.GetComponent<Dialogue>().StartDialogue();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            InteractButton.interactable = true;
            toInteract = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            InteractButton.interactable = false;
            toInteract = null;
        }
    }
}
