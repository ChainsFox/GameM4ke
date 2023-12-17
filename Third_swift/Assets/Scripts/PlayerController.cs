using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool IsMoving{get; private set;}
    [Header("Horizontal Movement")]
    //private float horizontal;
    Vector2 horizontal;
    public float walkSpeed = 5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        rb.velocity = new Vector2(horizontal.x * walkSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        horizontal = value.ReadValue<Vector2>();

        IsMoving = horizontal != Vector2.zero;
    }



}
