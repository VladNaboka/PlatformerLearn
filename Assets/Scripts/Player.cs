using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D playerColl;
    private Animator anim;
    private SpriteRenderer flip;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundJump;

    private enum AnimationState{ idle, move, jump, fall}
    AnimationState state;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerColl = rb.GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        flip = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
            state = AnimationState.move;
            flip.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
            state=AnimationState.move;
            flip.flipX = true;
        }
        else
        {
            state = AnimationState.idle;
        }
        anim.SetInteger("state", (int)state);

        inputVector = inputVector.normalized;

        Vector3 moveDir = new Vector3(inputVector.x * moveSpeed, 0f, 0f);

        transform.position += moveDir * Time.deltaTime;
    }

    private void Jump()
    { 
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(0, jumpForce);
        }
        if(rb.velocity.y > .1f)
        {
            state = AnimationState.jump;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = AnimationState.fall;
        }
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(playerColl.bounds.center, playerColl.bounds.size, 0f, Vector2.down, .1f, groundJump);
    }
}
