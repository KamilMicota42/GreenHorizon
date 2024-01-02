using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

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


    private enum MovementState {idle, running, jumping, falling};

    // Start is called before the first frame update
    private void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
        coll =  GetComponent<BoxCollider2D>();
        sprite =  GetComponent<SpriteRenderer>();
        anim =  GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {

        if(isDashing)
        {
            return;
        }

        moveHorizontally = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        
        if(moveHorizontally != 0f && speed < maxSpeed)
        {
            speed += Time.deltaTime * accelerationSpeed;
        }

        if(moveHorizontally == 0f)
        {
            speed = 0f;
        }


        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            Jump();
        }

        if(Input.GetButton("Jump") && isJumping)
        {
            if(jumpTimeCounter > 0)
            {
                Jump();
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if(Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
        
        if(Input.GetButtonDown("Dash") && canDash)
        {
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

        rb.velocity = new Vector3(speed * moveHorizontally * Time.deltaTime, rb.velocity.y, 0f);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(moveHorizontally * dashingPower, moveVertical * dashingPower*.75f);
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

        if(rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if(rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }
}
