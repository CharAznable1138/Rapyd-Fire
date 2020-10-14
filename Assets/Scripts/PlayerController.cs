using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Credit for raycast jumping logic: https://kylewbanks.com/blog/unity-2d-checking-if-a-character-or-object-is-on-the-ground-using-raycasts

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10;

    [SerializeField]
    private float jumpForce = 10;

    [SerializeField]
    private float groundCheckDistance = 1;

    [SerializeField]
    private float cooldownTime = 0.5f;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private LayerMask groundLayer;

    private Vector2 jumpVector;
    private float horizontalInput;
    private float verticalInput;
    private bool canShoot = true;
    internal bool FacingRight { get; private set; }
    internal bool AimingUp { get; private set; }
    internal bool AimingDown { get; private set; }

    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        AimingDown = false;
        AimingUp = false;
    }

    private bool IsOnGround()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = groundCheckDistance;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if(hit.collider != null)
        {
            return true;
        }
        return false;

    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if (horizontalInput != 0)
        {
            Move();
        }
        if(verticalInput != 0)
        {
            Aim();
        }
        else
        {
            AimingDown = false;
            AimingUp = false;
        }
        if(Input.GetKeyDown("space") && IsOnGround())
        {
            Jump();
        }
        if(Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine("Shoot");
        }
    }

    private IEnumerator Shoot()
    {
        Instantiate(bulletPrefab, gameObject.transform);
        canShoot = false;
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }

    private void Move()
    {
        if(FacingRight && horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
            FacingRight = false;
        }
        if(!FacingRight && horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
            FacingRight = true;
        }
        rigidbody2D.velocity = new Vector2(horizontalInput * movementSpeed, rigidbody2D.velocity.y);
    }

    private void Aim()
    {
        if(!IsOnGround() && verticalInput < 0)
        {
            AimingDown = true;
        }
        if(verticalInput > 0)
        {
            AimingUp = true;
        }
    }

    private void Jump()
    {
        jumpVector = new Vector2(0, jumpForce);
        rigidbody2D.AddForce(jumpVector);
    }
}
