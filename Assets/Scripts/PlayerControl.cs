using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Player Settings")]
    public float runSpeed = 50f;
    public float jumpPower = 400f;
    public float crouchSpeed = 0.6f;
    [Range(0, .5f)] public float moveSmoothing = 0.1f;
    
    [Header("Components")]
    public Rigidbody2D rigidBody;
    public Animator animator;
    
    [Header("GroundCheck")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    [Header("GameManager")] public GameManager gm;

    private float horizontalMove = 0f;
    private bool isJumping = false;
    private bool isCrouching = false;
    private Vector3 curVelocity= Vector3.zero;
    private bool facingRight = true;
    
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed",Math.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
        
    }

    private void FixedUpdate()
    {
        Move(horizontalMove * Time.fixedDeltaTime, isJumping, isCrouching);
        isJumping = false;
        
        animator.SetBool("Jump",!IsGrounded());
        
    }

    void Move(float move, bool jump, bool crouch)
    {
        if (crouch)
        {
            move *= crouchSpeed;
        }
            
        Vector3 targetVelocity = new Vector2(move * 10f, rigidBody.velocity.y);
        rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref curVelocity, moveSmoothing);

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }
            
        // If the player should jump...
        if (jump)
        {
            if (IsGrounded())
            {
                rigidBody.AddForce(new Vector2(0f,jumpPower));
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
    
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "MapBottom")
        {
            gm.GameEnd();
        }
        else if (col.tag=="Coin")
        {
            gm.PickupCoin();
        }
        else if (col.tag=="Torch")
        {
            gm.PickupTorch();
        }
    }
}
