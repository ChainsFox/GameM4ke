using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Horizontal Movement")]
    //private float horizontal;
    //Vector2 horizontal;
    Vector2 moveInput;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;

    public float CurrentMoveSpeed
    {
        get
        {
            if(IsMoving)
            {
                if(IsRunning) 
                {
                    return runSpeed;
                }
                else
                {
                    return walkSpeed;
                }
            }
            else
            {
                //idle speed = 0;
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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() //when you want to do a physics update, generally with rigidbody, you do it in fixed update.
    {
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
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



}
