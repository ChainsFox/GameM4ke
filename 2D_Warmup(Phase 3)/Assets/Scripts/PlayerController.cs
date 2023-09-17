using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //BASIC:
    public PlayerLife PL;
    public Rigidbody2D rb;
    private BoxCollider2D coll;
    public ParticleSystem dust;
    private Animator anim;
    private bool isFacingRight = true;
    public StopWatch stopWatch;
    private enum MovementState { idle, running, jumping, falling }; //animation

    //WALL SLIDING/JUMPING:
    [Header("Wall Slide/Jump")]
    private bool isWallSliding;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    private bool isWallJumping;
    private float wallJumpingDirection;
    public float wallJumpingTime = 0.6f;//kinda like jump buffering?
    private float wallJumpingCounter;
    public float wallJumpingDuration = 1f;
    public float wallSlidingSpeed = 2f;
    public Vector2 wallJumpingPower = new Vector2(8f, 16f);

    //MOVEMENT:
    [Header("Movement System")]
    [SerializeField] private float moveSpeed = 7f; //[SerializeField] so that you can change these numbers in unity edtior
    [SerializeField] private float jumpTime = 0.4f;
    [SerializeField] private float jumpForce = 13f;
    [SerializeField] private float fallMultiplier = 0.6f;
    [SerializeField] private float jumpMultiplier = 1f;
    [SerializeField] private LayerMask jumpableGround;
    bool isJumping;
    float jumpCounter;
    Vector2 vecGravity;


    //NEW INPUT SYSTEM VARIABLES:
    //Vector2 vecMove;
    private float horizontal;
    private PlayerInput playerInput;

    //COYOTE JUMP/JUMP BUFFERING VARIABLES:
    //[SerializeField]private float coyoteTime = 0.2f; //the higher the value the longer the player can jump after leaving the ground
    //private float coyoteTimeCounter;
    //[SerializeField] private float jumpBufferTime = 0.4f;
    //private float jumpBufferTimeCounter;


    //MAIN:
    void Start ()
    {
        Time.timeScale = 1f;
        GameManagerScript.GameIsPaused = false;
        PL = GetComponent<PlayerLife>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        stopWatch.timerActive = false;

        //playerInput = GetComponent<PlayerInput>(); //test

        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        Audio_Manager.Instance.PlaySFX("RespawnSFX");

    }

    private void Update ()
    {
        if (GameManagerScript.GameIsPaused == true)
            return;
        //Vector2 input = playerInput.actions["Movement"].ReadValue<Vector2>();
        //rb.velocity = new Vector2(input.x * moveSpeed, rb.velocity.y); //this dont work cuz its a vector 2 value in the editor?

        //if (playerInput.actions["Jump"].triggered && IsGrounded()) //this does work tho, note that its a button
        //{
        //    Audio_Manager.Instance.PlaySFX("JumpSFX");
        //    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //    isJumping = true;
        //    jumpCounter = 0;
        //    //jumpBufferTimeCounter = 0f;
        //    CreateDust();
        //}


        UpdateAnimationState();
        WallSlide();
        //WallJump();

        if (!isWallJumping) //We dont want our player to flip when walljumping, you have to look into this later
        {
            Flip(); //1 Second cool down for the player to wait before wall jumping again in the same wall/direction, so the player is stuck in the direction of the jump.
        }

        if (IsGrounded())
        {
            Flip(); //when the player touch the ground we can flip the player, so that we dont have to wait for that 1 second cool down.
        }

    }

    private void FixedUpdate ()
    {
        //rb.velocity = new Vector2(vecMove.x * moveSpeed, rb.velocity.y);
        if (PL.IsDead == false)
        {
             rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        }
        //rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        //Smooth jump logic:
        if (rb.velocity.y < -.1f)
        {
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }

        if (rb.velocity.y > .1f && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime)
                isJumping = false;

            float t = jumpCounter / jumpTime;
            float currentJumpM = jumpMultiplier;

            if (t > 0.5f)
            {
                currentJumpM = jumpMultiplier * (1 - t);
            }

            rb.velocity += vecGravity * currentJumpM * Time.deltaTime;
        }

    }
    

    //NEW INPUT SYSTEM FUNCTIONS:
    public void Jump (InputAction.CallbackContext value)
    {
        if (GameManagerScript.GameIsPaused == true)
            return;
        //if(IsGrounded())
        //{
        //    coyoteTimeCounter = coyoteTime;
        //}
        //else
        //{
        //    coyoteTimeCounter = -Time.deltaTime;
        //}

        //if (value.started)
        //{
        //    jumpbuffertimecounter = jumpbuffertime;
        //}
        //else
        //{
        //    jumpbuffertimecounter = -time.deltatime;
        //}


        if (value.started && IsGrounded() ) //coyoteTimeCounter > 0f && jumpBufferTimeCounter > 0f
        {
            Audio_Manager.Instance.PlaySFX("JumpSFX");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
            jumpCounter = 0;
            //jumpBufferTimeCounter = 0f;
            CreateDust();
        }
        if (value.canceled)
        {
            isJumping = false;
            jumpCounter = 0;

            if (rb.velocity.y > .1f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.6f);
            }
            //coyoteTimeCounter = 0f; //prevent the player from double jumping by spamming the jump button
        }

        if (rb.velocity.y < -.1f)
        {
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }

    }

    public void WallJump (InputAction.CallbackContext value)
    {
        //COYOTE JUMP/JUMP BUFFERING:

        //Wall jumping logic:
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x; //to jump facing the opposite direction, !!!you might need to change this if you have a different logic for flipping the player model
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime; //this allow us to turn away from the wall and still be able to wall jump for a brief moment
        }
        //we can use performed, but its not as snappy, and if we use started, we can jump infinitely on a wall, also audio bug/.
        if (value.performed && wallJumpingCounter > 0f) //wallJumpingCounter = wallJumpingTime = 0.2f, when wall sliding. //value.started or jumpBufferTimeCounter > 0f
        {
            Audio_Manager.Instance.PlaySFX("JumpSFX");
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y); //apply new velocity
            wallJumpingCounter = 0f; //to prevent the player from jumping multiple time
            //jumpBufferTimeCounter = 0f;
            CreateDust();
           

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;

            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration); //to stop the player
        }
    }

    public void Movement (InputAction.CallbackContext value)
    {
        //vecMove = value.ReadValue<Vector2>();
        horizontal = value.ReadValue<Vector2>().x;

    }

    private bool IsWalled ()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.3f, wallLayer); //default 0.2f
        //return Physics2D.OverlapBox(wallCheck.position, new Vector2(0.3f, 1.6f), 0, wallLayer); 
        //to check if we touching a wall. This method create an invisible circle with a radius of 0.2 at the position of the wallcheck and return true if it collide with the wall/(any set) layer.

    }

    private void WallSlide ()
    {
        if (IsWalled() && !IsGrounded() && horizontal !=0f)/*vecMove != 0f)*/
        {
            anim.SetBool("IsWallSliding", true);
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue)); //Clamp la kep, sliding logic
        }
        else
        {
            anim.SetBool("IsWallSliding", false);
            isWallSliding = false;
        }
    }

    private void StopWallJumping ()
    {
        isWallJumping = false;
    }

    private void UpdateAnimationState () //crtl +r +r to rename all spot
    {
        MovementState state;

        if (horizontal > 0f)
        {
            //anim.SetBool("running", true);*/ //"running" is the name of the animator parameter, true is the condition (deleted)
            state = MovementState.running;
            //sprite.flipX = false; //flipX is used to be bug, but i change the float dirX -> dirX only, and it works. Idk how tho, my bad.
            //There a function for flipping now, pretty neat
            stopWatch.timerActive = true;

        }
        else if (horizontal < 0f)
        {
            state = MovementState.running;
            //sprite.flipX = true;
            stopWatch.timerActive = true;


        }
        else
        {
            state = MovementState.idle;
            //anim.SetBool("running", false); old code
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
            stopWatch.timerActive = true;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state); //to change state to int 

    }

    //SMALL FUNCTIONS:
    public bool IsGrounded ()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        // this code is like we creating a box around our player the same size in the editor, and we check if the box is overlaping with the ground. Return true or false
        // Choose layer mask to check if it is grounded.
    }

    private void CreateDust ()
    {
        dust.Play();
    }

    private void Flip ()
    {
        if (isFacingRight &&  horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


}
