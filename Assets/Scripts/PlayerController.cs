using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10;
    [SerializeField] private float jumpForce = 10;
    private float horizontalInput;
    private bool isOnGround = false;

    private Rigidbody2D rigidbody2D;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rigidbody2D.velocity = new Vector2(horizontalInput * movementSpeed , rigidbody2D.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
