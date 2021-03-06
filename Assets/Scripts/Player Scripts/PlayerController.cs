﻿using System.Collections;
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
    [Tooltip("Speed at which the player moves. (Float)")]
    private float movementSpeed = 10;

    [SerializeField]
    [Tooltip("Maximum speed at which the player can move. (Float)")]
    private float maxSpeed = 20;

    [SerializeField]
    [Tooltip("Force with which the player jumps. (Float)")]
    private float jumpForce = 10;

    [SerializeField]
    [Tooltip("Length of raycast that checks if the player is on the ground. (Float)")]
    private float groundCheckDistance = 1;

    [SerializeField]
    [Tooltip("Amount of time after firing a bullet until player can fire another bullet. (Float)")]
    private float cooldownTime = 0.5f;

    [SerializeField]
    [Tooltip("Amount of time that Shield powerup lasts. (Float)")]
    private float shieldTime = 8;

    [SerializeField]
    [Tooltip("Amount of time for each flash to last during shield expiry. (Float)")]
    private float shieldExpiryFlashTime = 0.2f;

    [SerializeField]
    [Tooltip("Amount of flashes to occur during shield expiry. (Integer)")]
    private int shieldExpiryFlashes = 10;

    [SerializeField]
    [Tooltip("Bullet prefab to be instantiated when the player fires their weapon.")]
    private GameObject bulletPrefab;

    [SerializeField]
    [Tooltip("Layer on which to check for the ground. (LayerMask)")]
    private LayerMask groundLayer;

    [SerializeField]
    [Tooltip("Layer on which to check for enemies. (LayerMask)")]
    private LayerMask enemyLayer;

    [SerializeField]
    [Tooltip("Amount of points to be awarded to the player if a Shield powerup is collected.")]
    private float shieldGetPoints = 5;

    [SerializeField]
    [Tooltip("Color the player will turn into while shielded.")]
    private Color32 powerupSpriteColor = new Color32(255, 25, 155, 255);

    [SerializeField]
    [Tooltip("The sound to be played when the Player jumps.")]
    private AudioClip jumpSound;

    [Tooltip("The Animator component attached to this game object.")]
    private Animator animator;
    [Tooltip("The Sound Manager game object.")]
    private GameObject soundManagerObject;
    [Tooltip("The SoundManager script attached to the Sound Manager game object.")]
    private SoundManager soundManagerScript;
    [Tooltip("Direction in which the player will jump. (Vector2)")]
    private Vector2 jumpVector;
    [Tooltip("Positive number = player is inputting to the right, Negative number = player is inputting to the left. (Float)")]
    private float horizontalInput;
    [Tooltip("Positive number = player is inputting upward, Negative number = player is inputting downward. (Float)")]
    private float verticalInput;
    [Tooltip("True = Player can shoot, False = Player cannot shoot. (Bool)")]
    private bool canShoot = true;
    [Tooltip("True = Player is facing right, False = Player is facing left. (Bool)")]
    private bool facingRight;
    [Tooltip("True = Player's movement is locked, False = Player's movement is not locked. (Bool)")]
    private bool locked = false;
    [Tooltip("True = Player is midjump, False = Player is not midjump. (Bool)")]
    private bool isJumping = false;
    [Tooltip("The empty game object which tracks the Player's score.")]
    private GameObject scoreTrackerObject;
    [Tooltip("The ScoreTracker script attached to the Score Tracker game object.")]
    private ScoreTracker scoreTrackerScript;
    [Tooltip("The Player's default color scheme.")]
    private Color32 normalSpriteColor;
    [Tooltip("The 2D Collider component attached to the Player game object.")]
    private Collider2D collider2D;
    /// <summary>
    /// The amount of time that the Player's Shield powerup lasts. (Float)
    /// </summary>
    internal float ShieldTime { get { return shieldTime; } }
    /// <summary>
    /// Check if the player is currently shielded. True = Yes, False = No. (Bool)
    /// </summary>
    internal bool IsShielded { get; private set; }
    /// <summary>
    /// While Shield Expiry is flashing, check whether the flash is currently on or off. (Bool, Readonly)
    /// </summary>
    internal bool ShieldExpiryFlashIsOn { get; private set; }
    /// <summary>
    /// List of possible player actions.
    /// </summary>
    internal enum PlayerActions
    {
        Move,
        Jump,
        Shoot,
        Aim,
        Lock
    }
    /// <summary>
    /// List of actions the player has already taken. (Readonly)
    /// </summary>
    internal readonly List<PlayerActions> PlayerHasAlreadyDone = new List<PlayerActions>();
    /// <summary>
    /// The directions in which the Player can aim their weapon.
    /// </summary>
    internal enum AimingDirectionState
    {
        Up,
        Down,
        Left,
        Right,
        UpLeft,
        UpRight
    }
    /// <summary>
    /// The direction in which the Player is currently aiming their weapon. (Readonly)
    /// </summary>
    internal AimingDirectionState AimingDirection { get; private set; }

    [Tooltip("The Rigidbody2D component attached to the Player's game object.")]
    private Rigidbody2D rigidbody2D;
    [Tooltip("The SpriteRenderer component attached to the Player's game object.")]
    private SpriteRenderer spriteRenderer;
    [Tooltip("The UI object that shows how much time is left for the Player's current shield powerup.")]
    private GameObject shieldTimer;

    private void Start()
    {
        AssignComponents();
        SetNormalColorScheme();
        InitializeBools();
        FindScoreTracker();
    }
    /// <summary>
    /// Set initival values for all boolean variables attached to this script.
    /// </summary>
    private void InitializeBools()
    {
        facingRight = true;
        isJumping = false;
        IsShielded = false;
        ShieldExpiryFlashIsOn = false;
    }

    /// <summary>
    /// Set the player's normal color scheme to whatever color scheme is currently being used by the player prefab.
    /// </summary>
    private void SetNormalColorScheme()
    {
        normalSpriteColor = spriteRenderer.color;
    }

    /// <summary>
    /// Find the Score Tracker game object and its attached Score Tracker script.
    /// </summary>
    private void FindScoreTracker()
    {
        scoreTrackerObject = GameObject.FindGameObjectWithTag("Score Tracker");
        scoreTrackerScript = scoreTrackerObject.GetComponent<ScoreTracker>();
        soundManagerObject = GameObject.FindGameObjectWithTag("Sound Manager");
        soundManagerScript = soundManagerObject.GetComponent<SoundManager>();
    }

    /// <summary>
    /// Set values for the components this script needs to communicate with.
    /// </summary>
    private void AssignComponents()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
    }

    /// <summary>
    /// Check whether the player is currently on the ground. True = yes, False = no.
    /// </summary>
    /// <returns></returns>
    private bool IsOnGround()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = groundCheckDistance;

        //Shoutout to Brendan Lienau for teaching me how to use collider2D.bounds to get the exact proportions of the collider rather than guessing

        Vector2 position2 = collider2D.bounds.min;

        Vector2 position3 = new Vector2(collider2D.bounds.max.x, collider2D.bounds.min.y);

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(position2, direction, distance, groundLayer);
        RaycastHit2D hit3 = Physics2D.Raycast(position3, direction, distance, groundLayer);
        if (hit.collider != null || hit2.collider != null || hit3.collider != null)
        {
            return true;
        }
        return false;

    }

    /// <summary>
    /// Check whether the player is currently standing atop an enemy. True = yes, False = no.
    /// </summary>
    /// <returns></returns>
    private bool IsOnEnemy()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = groundCheckDistance;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, enemyLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;

    }

    private void Update()
    {
        if (!GameIsPaused())
        {
            SetAimingDirection();
            HandleJumpInput();
            HandleShootInput();
            HandleLockMovementInput();
            AnimateFalling();
        }
    }
    /// <summary>
    /// Returns true if the game is currently paused, otherwise returns false.
    /// </summary>
    /// <returns></returns>
    private bool GameIsPaused()
    {
        if (Time.timeScale > 0)
        {
            return false;
        }
        return true;
    }
    /// <summary>
    /// Lock the player's movement if the player right-clicks and is on the ground.
    /// </summary>
    private void HandleLockMovementInput()
    {
        if (Input.GetMouseButton(1) && (IsOnGround() || IsOnEnemy()))
        {
            locked = true;
            AnimateIdle();
            AddToPlayerHasAlreadyDoneList(PlayerActions.Lock);
        }
        else
        {
            locked = false;
        }
    }
    /// <summary>
    /// Set the player's animator to the Idle state, provided the player is grounded.
    /// </summary>
    private void AnimateIdle()
    {
        if (IsOnGround() || IsOnEnemy())
        {
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
        }
    }
    /// <summary>
    /// Set the player's animator to the Running state, provided the player is grounded.
    /// </summary>
    private void AnimateRunning()
    {
        if (IsOnGround() || IsOnEnemy())
        {
            animator.SetBool("IsMoving", true);
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
        }
    }
    /// <summary>
    /// Set the player's animator to the Jumping state.
    /// </summary>
    private void AnimateJumping()
    {
        animator.SetBool("IsJumping", true);
        animator.SetBool("IsFalling", false);
    }
    /// <summary>
    /// Set the player's animator to the Falling state, provided the player isn't grounded and has a negative y-velocity.
    /// </summary>
    private void AnimateFalling()
    {
        if (!(IsOnGround() || IsOnEnemy()) && rigidbody2D.velocity.y < 0)
        {
            animator.SetBool("IsFalling", true);
            animator.SetBool("IsJumping", false);
        }
    }

    /// <summary>
    /// Fire the player's weapon if the player left-clicks and canShoot is true.
    /// </summary>
    private void HandleShootInput()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine("Shoot");
        }
    }

    /// <summary>
    /// Set "isJumping" to true if the player presses space and is currently on the ground.
    /// </summary>
    private void HandleJumpInput()
    {
        if (Input.GetKeyDown("space") && (IsOnGround() || IsOnEnemy()))
        {
            isJumping = true;
        }
    }

    /// <summary>
    /// Set the player's aiming direction and, if it isn't left or right and HasAimed is false, set HasAimed to true.
    /// </summary>
    private void SetAimingDirection()
    {
        AimingDirection = Aim();
        if (AimingDirection != AimingDirectionState.Right && AimingDirection != AimingDirectionState.Left)
        {
            AddToPlayerHasAlreadyDoneList(PlayerActions.Aim);
        }
    }

    private void FixedUpdate()
    {
        HandleDirectionalInput();
        MovePlayer();
        MakePlayerJump();
    }
    /// <summary>
    /// Call the Jump method if isJumping is true, then set isJumping to false.
    /// </summary>
    private void MakePlayerJump()
    {
        if (isJumping)
        {
            Jump();
            isJumping = false;
        }
    }

    /// <summary>
    /// Apply directional force to the player's game object according to the player's directional input.
    /// </summary>
    private void MovePlayer()
    {
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            if (facingRight)
            {
                Flip();
            }
            if (rigidbody2D.velocity.x > -maxSpeed && !locked)
            {
                rigidbody2D.AddForce(Vector2.left * movementSpeed * Mathf.Abs(horizontalInput));
                AnimateRunning();
                AddToPlayerHasAlreadyDoneList(PlayerActions.Move);
            }
        }
        else if (Input.GetKey("right") || Input.GetKey("d"))
        {
            if (!facingRight)
            {
                Flip();
            }
            if (rigidbody2D.velocity.x < maxSpeed && !locked)
            {
                rigidbody2D.AddForce(Vector2.right * movementSpeed * Mathf.Abs(horizontalInput));
                AnimateRunning();
                AddToPlayerHasAlreadyDoneList(PlayerActions.Move);
            }
        }
        else
        {
            if (IsOnGround() || IsOnEnemy())
            {
                AnimateIdle();
            }
        }
    }

    /// <summary>
    /// Set the values of horizontalInput and verticalInput according to which directional buttons the player currently is (or isn't) pressing.
    /// </summary>
    private void HandleDirectionalInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    /// <summary>
    /// Fire the Player's weapon.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Shoot()
    {
        Instantiate(bulletPrefab, gameObject.transform);
        AddToPlayerHasAlreadyDoneList(PlayerActions.Shoot);
        canShoot = false;
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }
    /// <summary>
    /// Change which direction the Player is currently facing.
    /// </summary>
    private void Flip()
    {
        if (facingRight)
        {
            spriteRenderer.flipX = true;
            facingRight = false;
        }
        else
        {
            spriteRenderer.flipX = false;
            facingRight = true;
        }
    }
    /// <summary>
    /// Make the Player jump.
    /// </summary>
    private void Jump()
    {
        jumpVector = new Vector2(0, jumpForce);
        soundManagerScript.PlaySound(jumpSound, 0.5f);
        rigidbody2D.AddForce(jumpVector, ForceMode2D.Force);
        AnimateJumping();
        AddToPlayerHasAlreadyDoneList(PlayerActions.Jump);
    }
    /// <summary>
    /// Add a player action to the list of actions the player has already taken,
    /// unless said list already contains said player action.
    /// </summary>
    /// <param name="_playerAction">The player action to be added to the list.</param>
    private void AddToPlayerHasAlreadyDoneList(PlayerActions _playerAction)
    {
        if (!PlayerHasAlreadyDone.Contains(_playerAction))
        {
            PlayerHasAlreadyDone.Add(_playerAction);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartShieldCoroutineIfTouchingShield(collision);
    }
    /// <summary>
    /// Check if the player just touched a shield powerup, and start the shield coroutine if so.
    /// </summary>
    /// <param name="collision">The collider that the player just touched.</param>
    private void StartShieldCoroutineIfTouchingShield(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            ResetShield();
            StartCoroutine("CherryShieldGet");
        }
    }
    /// <summary>
    /// Forces all functions related to player's shield powerup to shut down.
    /// Useful for handling complications that can arise when player grabs a shield powerup while another shield powerup is still counting down.
    /// </summary>
    private void ResetShield()
    {
        spriteRenderer.color = normalSpriteColor;
        ShieldExpiryFlashIsOn = false;
        IsShielded = false;
        if (ShieldTimerExists())
        {
            Destroy(shieldTimer);
        }
        StopAllCoroutines();

    }
    /// <summary>
    /// Returns true if a shield timer exists in the scene, otherwise returns false.
    /// </summary>
    /// <returns></returns>
    private bool ShieldTimerExists()
    {
        shieldTimer = GameObject.FindGameObjectWithTag("Shield Timer");
        if (shieldTimer != null)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Activate the Player's Shield powerup, and let other scripts know that the powerup is active.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CherryShieldGet()
    {
        if (!IsShielded)
        {
            spriteRenderer.color = powerupSpriteColor;
            scoreTrackerScript.Score += shieldGetPoints;
            IsShielded = true;
        }
        yield return new WaitForSeconds(shieldTime);
        StartCoroutine("CherryShieldExpiry");
    }
    /// <summary>
    /// Flash the player's color scheme between normal and powered up in timed bursts a specified amount of times, then set colors to normal and let other scripts know that the powerup is no longer active.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CherryShieldExpiry()
    {
        for (int i = 0; i < shieldExpiryFlashes; i++)
        {
            SwitchColors();
            yield return new WaitForSeconds(shieldExpiryFlashTime);
        }
        spriteRenderer.color = normalSpriteColor;
        IsShielded = false;
    }
    /// <summary>
    /// Aim the player's weapon in a specified direction.
    /// </summary>
    /// <returns></returns>
    private AimingDirectionState Aim()
    {
        if (verticalInput > 0)
        {
            if (horizontalInput > 0)
            {
                return AimingDirectionState.UpRight;
            }
            else if (horizontalInput < 0)
            {
                return AimingDirectionState.UpLeft;
            }
            else
            {
                return AimingDirectionState.Up;
            }
        }
        else if (verticalInput < 0 && (!IsOnGround() || !IsOnEnemy()))
        {
            return AimingDirectionState.Down;
        }
        else
        {
            if (facingRight)
            {
                return AimingDirectionState.Right;
            }
            else
            {
                return AimingDirectionState.Left;
            }
        }
    }
    /// <summary>
    /// Switch the player's color scheme from normal to powered up, or vice versa.
    /// </summary>
    private void SwitchColors()
    {
        if (spriteRenderer.color == normalSpriteColor)
        {
            spriteRenderer.color = powerupSpriteColor;
            ShieldExpiryFlashIsOn = false;
        }
        else if (spriteRenderer.color == powerupSpriteColor)
        {
            spriteRenderer.color = normalSpriteColor;
            ShieldExpiryFlashIsOn = true;
        }
    }
}
