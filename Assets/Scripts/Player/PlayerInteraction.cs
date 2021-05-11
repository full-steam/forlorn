using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{

    public Button interactButton;
    public GameObject toInteract;

    public void Interact(GameObject _toInteract = null)
    {
        //method will use the paramater as the item to be interacted, unless stated explicitly, will interact with existing reference in component
        if (_toInteract = null)
        {
            if (toInteract = null) { Debug.LogError("No object detected"); return; }
            _toInteract = toInteract;
        }

        Debug.Log("Please uncomment previoulsy missing reference. (Dialogue.cs)");
        //_toInteract.GetComponent<Dialogue>().StartDialogue();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            interactButton.interactable = true;
            toInteract = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            interactButton.interactable = false;
            toInteract = null;
        }
    }
}
