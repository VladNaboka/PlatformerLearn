using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D playerColl;
    private Animator anim;
    private bool facingRight = true;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("Ground Check System")]
    private bool isGrounded;
    [SerializeField] private LayerMask groundJump;
    [SerializeField] Transform groundCheck;


    [Header("Wall Jump System")]
    [SerializeField] Transform wallCheck;
    private bool isWallDetected;
    private bool isWallSliding;
    private bool canWallSlide;
    private bool canWallJump = true;
    [SerializeField] float wallSlideSpeed;
    [SerializeField] private LayerMask isWallLayer;
    private int facingDirection = 1;
    [SerializeField] private Vector2 wallJumpDirection;


    private enum AnimationState{ idle, move, jump, fall}
    AnimationState state;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerColl = rb.GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        FlipController();
        AnimPlayer();
        WallSlide();
        CheckCollision();
        WallSlide();
    }

    private void Update()
    {
        Jump();
        CanSlide();
    }
  
    private void Jump()
    {
        if (isWallSliding && canWallJump && Input.GetKeyDown(KeyCode.Space))
        {
            WallJump();
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(0, jumpForce);
            AudioManager.instance.PlaySFX("Jump");
        }

        canWallSlide = false;
    }

    private void CheckCollision()
    {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.73f, 0.23f), CapsuleDirection2D.Horizontal, 0 , groundJump);
        isWallDetected = Physics2D.OverlapCapsule(wallCheck.position, new Vector2(0.4f, 0.23f), CapsuleDirection2D.Horizontal, 0, isWallLayer);
    }

    private void Move()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }
    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    private void FlipController()
    {
        if (isGrounded && isWallDetected)
        {
            if(facingRight && dirX < 0)
            {
                Flip();
            }
            else if(!facingRight && dirX > 0)
            {
                Flip();
            }
        }

        if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }

    }

    private void WallSlide()
    {
        if (isWallDetected && canWallSlide && !isGrounded)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y * wallSlideSpeed);
        }
        else
        {
            isWallSliding = false;
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }
    }

    private void WallJump()
    {
        Vector2 direction = new Vector2(wallJumpDirection.x * -facingDirection, wallJumpDirection.y);

        rb.AddForce(direction, ForceMode2D.Impulse);
    }

    private void CanSlide()
    { 
        if (!isGrounded && rb.velocity.y < 0f)
        {
            canWallSlide = true;
        }
    }

    private void AnimPlayer()
    { 
        // Move animation
        if (rb.velocity.x < 0 && !isWallDetected)
        {
            state = AnimationState.move;
        }
        else if (rb.velocity.x > 0 && !isWallDetected)
        {
            state = AnimationState.move;
        }
        else
        {
            state = AnimationState.idle;
        }
        // Jump animation
        if (rb.velocity.y > .1f)
        {
            state = AnimationState.jump;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = AnimationState.fall;
        }

        // Wall Slide Animation
        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetInteger("state", (int)state);
    }
}
