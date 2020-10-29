using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private float powerupStrength = 2;

    [SerializeField]
    private float powerupTime = 8;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private float powerupPoints = 5;

    private Vector2 jumpVector;
    private float horizontalInput;
    private float verticalInput;
    private bool canShoot = true;
    private bool facingRight;
    private bool locked = false;
    private GameObject scoreTrackerObject;
    private ScoreTracker scoreTrackerScript;
    internal bool IsPoweredUp { get; private set; }
    internal enum AimingDirectionState
    {
        Up,
        Down,
        Left,
        Right,
        UpLeft,
        UpRight
    }
    internal AimingDirectionState AimingDirection { get; private set; }

    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        facingRight = true;
        scoreTrackerObject = GameObject.FindGameObjectWithTag("Score Tracker");
        scoreTrackerScript = scoreTrackerObject.GetComponent<ScoreTracker>();
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
        AimingDirection = Aim();
        if(Input.GetKeyDown("space") && IsOnGround())
        {
            Jump();
        }
        if(Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine("Shoot");
        }
        if(Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(Input.GetMouseButton(1) && IsOnGround())
        {
            locked = true;
            Freeze();
        }
        else
        {
            locked = false;
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
        if (!locked)
        {
            rigidbody2D.velocity = new Vector2(horizontalInput * movementSpeed, rigidbody2D.velocity.y);
        }
    }
    private void Freeze()
    {
        rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        jumpVector = new Vector2(0, jumpForce);
        rigidbody2D.AddForce(jumpVector);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Powerup"))
        {
            StartCoroutine("Powerup");
        }
    }

    private IEnumerator Powerup()
    {
        float normalCooldownTime = cooldownTime;
        cooldownTime /= powerupStrength;
        if (!IsPoweredUp)
        {
            scoreTrackerScript.Score += powerupPoints;
            IsPoweredUp = true;
        }
        yield return new WaitForSeconds(powerupTime);
        IsPoweredUp = false;
        cooldownTime = normalCooldownTime;
    }

    private AimingDirectionState Aim()
    {
        if(verticalInput > 0)
        {
            if(horizontalInput > 0)
            {
                return AimingDirectionState.UpRight;
            }
            else if(horizontalInput < 0)
            {
                return AimingDirectionState.UpLeft;
            }
            else
            {
                return AimingDirectionState.Up;
            }
        }
        else if(verticalInput < 0 && !IsOnGround())
        {
            return AimingDirectionState.Down;
        }
        else
        {
            if(facingRight)
            {
                return AimingDirectionState.Right;
            }
            else
            {
                return AimingDirectionState.Left;
            }
        }
    }
}
