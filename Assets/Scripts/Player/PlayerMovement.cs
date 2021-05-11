using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //public bl_Joystick Joystick { set; get; }
    public float moveSpeed = 5f;

    //for debugging purpose
    public bool useKeyboard = false;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    private bool canMove;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (useKeyboard)
            {
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
            }
            else
            {
                //TODO: ADD bl_Joystick

                //bl_Joystick's Horizontal/Vertical returns float from -1 to 1. Manually converted to -1, 0 or 1
                //movement.x = (joystick.Horizontal > 0.1) ? 1 : ((joystick.Horizontal < -0.1) ? -1 : 0);
                //movement.y = (joystick.Vertical > 0.1) ? 1 : ((joystick.Vertical < -0.1) ? -1 : 0);
            }
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetFloat("Speed", movement.sqrMagnitude);
        }
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void ToggleMovement(bool toggle)
    {
        canMove = toggle;
    }
}
