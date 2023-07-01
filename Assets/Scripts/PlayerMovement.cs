using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
                Vector2 inputVector = new Vector2(0, 0);

                if (Input.GetKey(KeyCode.D))
                {
                    inputVector.x += 1;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    inputVector.x -= 1;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    player.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                    
                 }

                inputVector = inputVector.normalized;

               transform.position += (Vector3)inputVector * Time.deltaTime * moveSpeed;

    }
}
