using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;                    //considering changing access modifier later if no need to make changes
    [SerializeField] private bl_Joystick joystick;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    private bool canMove;

    //for debugging purpose
    public bool useKeyboard = false;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        joystick = GameManager.Instance.Blackboard.Joystick;
        canMove = true;
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
                ////bl_Joystick's Horizontal/Vertical returns float from -1 to 1. Manually converted to -1, 0 or 1
                //movement.x = (joystick.Horizontal > 0.1) ? 1 : ((joystick.Horizontal < -0.1) ? -1 : 0);
                //movement.y = (joystick.Vertical > 0.1) ? 1 : ((joystick.Vertical < -0.1) ? -1 : 0);

                movement.x = joystick.Horizontal;
                movement.y = joystick.Vertical;
            }
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);

            #region Idle Direction
            //sets up idle direction
            // Down : 0
            // Left : 1
            // Up   : 2
            // Right: 3
            if (Mathf.Abs(movement.y) >= Mathf.Abs(movement.x))
            {
                if (movement.y < -0.1) anim.SetFloat("Direction", 0);
                else if (movement.y > 0.1) anim.SetFloat("Direction", 2);
            }
            else
            {
                if (movement.x < -0.1) anim.SetFloat("Direction", 1);
                else if (movement.x > 0.1) anim.SetFloat("Direction", 3);
            }
            #endregion

            movement.x = (movement.x > 0.1) ? 1 : ((movement.x < -0.1) ? -1 : 0);
            movement.y = (movement.y > 0.1) ? 1 : ((movement.y < -0.1) ? -1 : 0);

            //anim.SetFloat("Speed", movement.sqrMagnitude);
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