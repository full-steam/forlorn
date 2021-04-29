using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //public bl_Joystick Joystick { set; get; }
    public float moveSpeed;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMovement(bool toggle)
    {
        canMove = toggle;
    }
}
