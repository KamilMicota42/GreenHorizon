using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    private CheckpointMaster cm;

    public Joystick joystick;
    public bool isJumpPressed;
    public bool isDashPressed;

    [Header("Movement info")]
    [SerializeField] private float speed = 0f;
    [SerializeField] private float maxSpeed = 14f;
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float moveHorizontally;
    [SerializeField] private float moveVertical;
    
    [Header("Jumping info")]
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private float jumpTime;
    private float jumpTimeCounter;
    private bool isJumping;
    
    [Header("Dashing info")]
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    private bool canDash = true;
    private bool isDashing;

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded = true;

    [Header("Sounds")]
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource dashSound;

    private enum MovementState {idle, running, jumping, falling};

    // Start is called before the first frame update
    private void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
        coll =  GetComponent<BoxCollider2D>();
        sprite =  GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        cm = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckpointMaster>();
        transform.position = cm.lastCheckpointPosition;
    }

    // Update is called once per frame
    private void Update()
    {

        if(isDashing)
        {
            return;
        }

        moveHorizontally = joystick.Horizontal;
        moveVertical = joystick.Vertical;
        
        if(moveHorizontally != 0f && speed < maxSpeed)
        {
            speed += Time.deltaTime * accelerationSpeed;
        }

        if(moveHorizontally == 0f)
        {
            speed = 0f;
        }


        if(isJumpPressed && isGrounded)
        {
            jumpSound.Play();
            isJumping = true;
            jumpTimeCounter = jumpTime;
            
            Jump();
        }

        if(isJumpPressed && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                
                Jump();
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if(!isJumpPressed)
        {
            isJumping = false;
        }
        
        if(isDashPressed && canDash)
        {
            dashSound.Play();
            StartCoroutine(Dash());
        }

        if(isGrounded)
        {
            canDash = true;
        }

        CollisionCheck();
        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }

        rb.linearVelocity = new Vector3(speed * moveHorizontally * Time.deltaTime, rb.linearVelocity.y, 0f);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(moveHorizontally * dashingPower, moveVertical * dashingPower*.75f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
    }

    private void CollisionCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if(moveHorizontally > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        } 
        else if(moveHorizontally < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        } 
        else 
        {
            state = MovementState.idle;
        } 

        if(rb.linearVelocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if(rb.linearVelocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    public void HandleJumpInput(bool isPressed)
    {
        isJumpPressed = isPressed;
    }

    public void HandleDashInput(bool isPressed)
    {
        if(isPressed && canDash)
        {
            dashSound.Play();
            StartCoroutine(Dash());
        }
    }
}
