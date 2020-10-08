using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10;
    [SerializeField] private float jumpForce = 10;

    [SerializeField] private GameObject bulletPrefab;

    private float horizontalInput;
    private bool onGround = false;
    private bool facingRight = true;

    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            Move();
        }
        if(Input.GetKeyDown("space") && onGround)
        {
            onGround = false;
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void Move()
    {
        if(facingRight && horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
            facingRight = false;
        }
        if(!facingRight && horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
            facingRight = true;
        }
        rigidbody2D.velocity = new Vector2(horizontalInput * movementSpeed, rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
    }
}
