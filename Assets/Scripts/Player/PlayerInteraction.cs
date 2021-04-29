using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{

    public Button interactButton;
    public GameObject toInteract;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(GameObject _toInteract = null)
    {
        if (!_toInteract) _toInteract = toInteract;

        //IMPLEMENT
    }
}
