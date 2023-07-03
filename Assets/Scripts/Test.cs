using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private Animator anim;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private float dirX = 0f;

    private void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        FlipPlayer();
        AnimPlayer();
    }

    private void Move()
    {
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    private void FlipPlayer()
    {
        if(rb.velocity.x < 0)
        {
            sp.flipX = true;
        }
        else if(rb.velocity.x > 0)
        {
            sp.flipX = false;
        }
    }
    private void AnimPlayer()
    {
        if (rb.velocity.x < 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else if (rb.velocity.x > 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
    }
}
