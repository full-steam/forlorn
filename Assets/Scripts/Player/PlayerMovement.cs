using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;                    //considering changing access modifier later if no need to make changes
    [SerializeField] private bl_Joystick joystick;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    private bool canMove = true;
    private bool moving = false;

    //for debugging purpose
    [Tooltip("Will be automatically turned to true when playing from editor")]
    public bool useKeyboard = false;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
//#if UNITY_EDITOR
//        useKeyboard = true;
//#endif
    }

    private void Start()
    {
        joystick = GameManager.Instance.Blackboard.Joystick;
        //canMove = true;
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
                //Debug.Log(movement);
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

            //ADDED TO CHECK HOW SENSITIVE IT IS ON BUILT, CHANGE BACK TO 0.1f FOR EVERY CHECK IF WEIRD
            movement.x = (movement.x >= 1.0f) ? 1 : ((movement.x <= -1.0f) ? -1 : 0);
            movement.y = (movement.y >= 1.0f) ? 1 : ((movement.y <= -1.0f) ? -1 : 0);

            if (!moving && movement.sqrMagnitude > 0)
            {
                moving = true;
                AudioController.Play("WalkDirt");
            }
            else if (moving && movement.sqrMagnitude == 0)
            {
                moving = false;
                AudioController.Stop("WalkDirt");
            }

            //anim.SetFloat("Speed", movement.sqrMagnitude);
        }
        else
        {
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Vertical", 0);
            moving = false;
            AudioController.Stop("WalkDirt");
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