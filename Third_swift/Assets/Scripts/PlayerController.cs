using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))] 
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Horizontal Movement")]
    //private float horizontal;
    //Vector2 horizontal;
    Vector2 moveInput;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 7f;
    public float jumpImpulse = 10f;
    TouchingDirections touchingDiretions;

    public float CurrentMoveSpeed
    { 
        get
        {
            if(CanMove)
            {
                if (IsMoving && !touchingDiretions.IsOnWall)
                {
                    if (touchingDiretions.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {//Air move
                        return airWalkSpeed;
                    }

                }
                else
                {
                    //idle speed = 0;
                    return 0;
                }
            } 
            else
            {
                //movement locked
                return 0;
            }

          

            
        }
    }

    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving 
    { get 
        {
            return _isMoving;
        
        
        } 
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);



        }
    }

    public bool CanMove 
    { get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    Animator animator;


    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning 
    { 
        get 
        {
            return _isRunning;
        
        } 
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    
    
    }

    public bool _isFacingRight = true;
    public bool IsFacingRight 
    { 
        get { return _isFacingRight; } 
        private set 
        { 
            if(_isFacingRight != value)
            {
                //flip the local scale to make the player face the opposite direction
                transform.localScale *= new Vector2(-1,1);

            }
            _isFacingRight= value;
        
        } 
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDiretions = GetComponent<TouchingDirections>();
    }


    private void FixedUpdate() //when you want to do a physics update, generally with rigidbody, you do it in fixed update.
    {
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }

    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            //face right
            IsFacingRight = true;   
        }
        else if(moveInput.x < 0 && IsFacingRight)
        {
            //face left
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if(context.canceled)
        {
            IsRunning = false;

        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //TODO: Check if alive as well
        if(context.started && touchingDiretions.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
        
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }

    }


}
