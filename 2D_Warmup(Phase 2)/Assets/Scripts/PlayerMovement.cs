using Autodesk.Fbx;
using Mono.CompilerServices.SymbolWriter;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{

    public PlayerLife PL;
    public Rigidbody2D rb; //khai bao va dat ten
    private bool isFacingRight = true;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;

    public StopWatch stopWatch;//stopwatch
 
    private float dirX = 0f;
    [SerializeField] private LayerMask jumpableGround;

    [Header("Movement System")]
    [SerializeField] private float moveSpeed = 7f; //[SerializeField] so that you can change these numbers in unity edtior
    [SerializeField] private float jumpTime = 0.4f;
    [SerializeField] private float jumpForce = 13f;
    [SerializeField] private float fallMultiplier = 0.6f;
    [SerializeField] private float jumpMultiplier = 1f;

    bool isJumping;
    float jumpCounter;

    private enum MovementState {idle, running, jumping, falling};

    //[SerializeField] private AudioSource jumpSoundEffect; //we gonna use alot of sounds so we dont use the get component method, because we cant distinguish them.
    //[SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private AudioSource RespawnSFX;

    Vector2 vecGravity;

    //Wall sliding, jumping variable:
    private bool isWallSliding;
    public float wallSlidingSpeed = 2f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    private bool isWallJumping;
    private float wallJumpingDirection;
    public float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    public float wallJumpingDuration = 0.4f;
    public Vector2 wallJumpingPower = new Vector2(8f, 16f);
    //public Vector2 wallJumpingPower;

    void Start ()
    {
        GameManagerScript.GameIsPaused = false;
        PL = GetComponent<PlayerLife>();
        //Debug.Log("Game start!");
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        stopWatch.timerActive = false;  

        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        //RespawnSFX.Play();
        Audio_Manager.Instance.PlaySFX("RespawnSFX");
   
    }   

    void Update () //ctrl + k and then crtl + d to rearange code/ctrl + k + c de comment./ctrl + k + u de uncomment
    {
        if(GameManagerScript.GameIsPaused == true) return; //When the game is pause the player completely pauses.

        // dirX = Input.GetAxis("Horizontal"); it slide when you release the button(more realistic).The velocity gradually get to 0
        if (PL.IsDead == false)
        {
            dirX = Input.GetAxisRaw("Horizontal"); //with this it will get to 0 immediately. Also this is a local variable
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y); //rb.velocity.y so that you keep the velocity that this rigidbody already had the frame before, you dont want to reset it to 0
        }
      
        //7f is 7 float value

        if (Input.GetButtonDown("Jump") && IsGrounded()) //edit -> project editor -> input
        {
            //jumpSoundEffect.Play();
            //FindObjectOfType<AudioManager>().Play("JumpSFX"); 
            Audio_Manager.Instance.PlaySFX("JumpSFX");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); //vector3(x,y,z) horizontal,vertical, and depth-this only matter in 3d games; i have change this to vector 2
            isJumping = true;   
            jumpCounter = 0;
        
            
        }

        if(rb.velocity.y > .1f && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) isJumping = false;

            float t = jumpCounter / jumpTime; //smooth jump upward
            float currentJumpM = jumpMultiplier;

            if (t > 0.5f) //if the jump button is larger than 0.5f then the character will move upward more slowly
            {
                currentJumpM = jumpMultiplier * (1 - t);    
            }

            rb.velocity += vecGravity * currentJumpM * Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpCounter = 0;

            if(rb.velocity.y > .1f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.6f);
            }

        }
        
        if(rb.velocity.y < -.1f)
        {
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }

        

        UpdateAnimationState();

        WallSlide();
        WallJump();

        if (!isWallJumping) //!!! We dont want our player to flip when walljumping, you have to look into this later
        {
            Flip();
        }

        // Flip();

    }

    private void UpdateAnimationState() //crtl +r +r to rename all spot
    {
        MovementState state;

        if (dirX > 0f) 
        {
        /*    anim.SetBool("running", true);*/ //"running" is the name of the animator parameter, true is the condition (deleted)
            state = MovementState.running;
            //sprite.flipX = false; //flipX is used to be bug, but i change the float dirX -> dirX only, and it works. Idk how tho, my bad.
            //There a function for flipping now, pretty neat
            stopWatch.timerActive = true;

        }
        else if (dirX < 0f)
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

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        // this code is like we creating a box around our player the same size in the editor, and we check if the box is overlaping with the ground. Return true or false
    }

    private void Flip ()
    {
        if (isFacingRight && dirX < 0f || !isFacingRight && dirX > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


    //WALL JUMPING/SLIDING LOGIC:

    private bool IsWalled ()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
        //return Physics2D.OverlapBox(wallCheck.position, new Vector2(0.3f, 1.6f), 0, wallLayer); //I mess around with this later to create a better detection box

        //to check if we touching a wall. This method create an invisible circle with a radius of 0.2 at the position of the wallcheck and return true if it collide with the wall/(any set) layer.
    }

    private void WallSlide ()
    {
        if (IsWalled() && !IsGrounded() && dirX != 0f)
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

    private void WallJump ()
    {
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

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f) //wallJumpingCounter = wallJumpingTime = 0.2f, when wall sliding.
        {
            Audio_Manager.Instance.PlaySFX("JumpSFX");
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y); //apply new velocity
            wallJumpingCounter = 0f; //to prevent the player from jumping multiple time

            if (transform.localScale.x != wallJumpingDirection) //!!! You will have to change this due to different player flip logic
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;

            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }

    }

    private void StopWallJumping ()
    {
        isWallJumping = false;
    }






}
