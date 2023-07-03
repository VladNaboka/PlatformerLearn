using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer flip;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        flip = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
            anim.SetBool("IsRunning", true);
            flip.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
            anim.SetBool("IsRunning", true);
            flip.flipX = true;
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(0, jumpForce);
        }

        inputVector = inputVector.normalized;

        Vector3 moveDir = new Vector3(inputVector.x * moveSpeed, 0f, 0f);

        transform.position += moveDir * Time.deltaTime;
    }
}
