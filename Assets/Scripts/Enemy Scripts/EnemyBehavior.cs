using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The speed at which enemies move. (Float)")]
    private float movementSpeed = 10;

    [SerializeField]
    [Tooltip("Distance to check if enemy is on the ground. (Float)")]
    private float groundCheckDistance = 1;

    [SerializeField]
    [Tooltip("Distance to check if the enemy is touching a wall. (Float")]
    private float wallCheckDistance = 1;

    [SerializeField]
    [Tooltip("Distance to check for other enemies. (Float)")]
    private float enemyContactCheckDistance = 1;

    [SerializeField]
    [Tooltip("Time after shooting before enemy can shoot again. (Float)")]
    private float cooldownTime = 0.5f;

    [SerializeField]
    [Tooltip("Time between when enemy detects player and when enemy can start shooting. (Float)")]
    private float initialDelayTime = 0.1f;

    [SerializeField]
    [Tooltip("Distance at which enemy can detect player. (Float)")]
    private float firingRange = 10;

    [SerializeField]
    [Tooltip("Bullet prefab to be instantiated.")]
    private GameObject bulletPrefab;

    [SerializeField]
    [Tooltip("Layer on which to check for the ground object. (LayerMask)")]
    private LayerMask groundLayer;

    [SerializeField]
    [Tooltip("Layer on which to check for the player object. (LayerMask)")]
    private LayerMask playerLayer;

    [SerializeField]
    [Tooltip("Layer on which to check for other enemies. (LayerMask)")]
    private LayerMask enemyLayer;

    [SerializeField]
    [Tooltip("The maximum distance the enemy will fly from its original position before turning around.")]
    private float maximumFlightDistance = 10;

    [Tooltip("The value of this enemy's initial x-position.")]
    private float startingPositionX;

    [Tooltip("True = enemy can shoot, False = enemy can't shoot.")]
    private bool canShoot = true;

    [Tooltip("True = enemy can switch which direction it's facing, False = enemy cannot do this.")]
    private bool canFlip = true;

    [SerializeField]
    [Tooltip("The minimum amount of time an enemy must wait since the last time it flipped directions before flipping again.")]
    private float flipCooldown = 0.1f;

    /// <summary>
    /// True = enemy is facing right, False = enemy is facing left
    /// </summary>
    internal bool FacingRight { get; private set; }
    /// <summary>
    /// Returns true if this enemy is a Flier type, otherwise returns false. (Readonly)
    /// </summary>
    internal bool EnemyTypeIsFlier
    {
        get { return IsFlier(); }
    }

    [Tooltip("The Rigidbody2D component attached to the Enemy prefab.")]
    private Rigidbody2D rigidbody2D;
    [Tooltip("The SpriteRenderer component attached to the Enemy prefab.")]
    private SpriteRenderer spriteRenderer;

    [Tooltip("The Jetpack game object that may or may not exist as a child of this enemy game object, if this enemy is a Flier.")]
    private GameObject jetpack;

    private void Start()
    {
        AssignComponents();
        InitializeBool();
        GetStartingPositionX();
    }
    /// <summary>
    /// Assign initial value for the boolean variable of this script.
    /// </summary>
    private void InitializeBool()
    {
        FacingRight = true;
    }
    /// <summary>
    /// Get this enemy game object's x-position.
    /// </summary>
    private void GetStartingPositionX()
    {
        startingPositionX = gameObject.transform.position.x;
    }
    /// <summary>
    /// Check the distance between the enemy's current x-position and its starting position.
    /// If it exceeds the maximum flight distance, return true.
    /// Otherwise, return false.
    /// </summary>
    /// <returns></returns>
    private bool HasReachedMaximumFlightDistance()
    {
        float distance = Math.Abs(gameObject.transform.position.x - startingPositionX);
        if (distance > maximumFlightDistance)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Get values for the components this script needs to communicate with.
    /// </summary>
    private void AssignComponents()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
        Attack();
    }
    /// <summary>
    /// Fire the enemy's weapon if the player is detected.
    /// </summary>
    private void Attack()
    {
        switch (IsFlier())
        {
            case false:
                if (canShoot && CanSeePlayerOnGround())
                {
                    StartCoroutine(Shoot());
                }
                break;
            case true:
                if (canShoot && CanSeePlayerFromAir())
                {
                    StartCoroutine(Shoot());
                }
                break;

        }
    }
    /// <summary>
    /// Determine if this particular enemy instance is a Flier type. 
    /// Returns true if this enemy has a Jetpack child object, otherwise returns false.
    /// </summary>
    /// <returns></returns>
    private bool IsFlier()
    {
        jetpack = GameObject.Find($"{this.name}/Jetpack");
        if (jetpack == null)
        {
            return false;
        }
        return true;
    }
    /// <summary>
    /// Check if the enemy can see the player on the ground. True = yes, False = no.
    /// </summary>
    /// <returns></returns>
    private bool CanSeePlayerOnGround()
    {
        Vector2 position = transform.position;
        Vector2 direction;
        float distance = firingRange;
        if (FacingRight)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, playerLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// Checks if the enemy can see the player from the air. True = yes, False = no.
    /// </summary>
    /// <returns></returns>
    private bool CanSeePlayerFromAir()
    {
        Vector2 position = transform.position;
        Vector2 direction;
        float distance = firingRange;
        if (FacingRight)
        {
            direction = Vector2.right + Vector2.down;
        }
        else
        {
            direction = Vector2.left + Vector2.down;
        }
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, playerLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;

    }
    /// <summary>
    /// Check if the enemy is on the edge of its current platform. True = yes, False = no.
    /// </summary>
    /// <returns></returns>
    private bool IsOnEdge()
    {
        Vector2 position = transform.position;
        Vector2 direction;
        if (FacingRight)
        {
            direction = Vector2.right - Vector2.up;
        }
        else
        {
            direction = Vector2.left - Vector2.up;
        }

        RaycastHit2D hit = Physics2D.Raycast(position, direction, groundCheckDistance, groundLayer);
        if (hit.collider == null)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// Check if the enemy is touching another enemy. True = yes, False = no.
    /// </summary>
    /// <returns></returns>
    private bool IsTouchingOtherEnemy()
    {
        Vector2 position = transform.position;
        Vector2 direction;
        if (FacingRight)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
        RaycastHit2D hit = Physics2D.Raycast(position, direction, enemyContactCheckDistance, enemyLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// Check if the enemy is touching a wall. True = yes, False = no.
    /// </summary>
    /// <returns></returns>
    private bool IsTouchingWall()
    {
        Vector2 position = transform.position;
        Vector2 direction;
        if (FacingRight)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
        RaycastHit2D hit = Physics2D.Raycast(position, direction, wallCheckDistance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// Fire the enemy's weapon.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Shoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(initialDelayTime);
        Instantiate(bulletPrefab, gameObject.transform);
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }
    /// <summary>
    /// Move the enemy.
    /// NOTE: This method behaves differently depending on whether or not the enemy is a Flier.
    /// </summary>
    private void Move()
    {
        switch (IsFlier())
        {
            case false:
                if (IsOnEdge() || IsTouchingOtherEnemy() || IsTouchingWall())
                {
                    Flip();
                }
                break;
            case true:
                if (HasReachedMaximumFlightDistance() || IsTouchingOtherEnemy() || IsTouchingWall())
                {
                    Flip();
                }
                break;
        }
        if (FacingRight)
        {
            rigidbody2D.velocity = new Vector2(movementSpeed, rigidbody2D.velocity.y);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(-movementSpeed, rigidbody2D.velocity.y);
        }
    }
    /// <summary>
    /// Change which direction the enemy is facing.
    /// </summary>
    private void Flip()
    {
        if (canFlip)
        {
            if (FacingRight)
            {
                spriteRenderer.flipX = true;
                FacingRight = false;
            }
            else
            {
                spriteRenderer.flipX = false;
                FacingRight = true;
            }
        }
        StartCoroutine(FlipCooldown());
    }
    /// <summary>
    /// Stop the enemy from flipping if it has already flipped within a specified timeframe.
    /// </summary>
    /// <returns></returns>
    private IEnumerator FlipCooldown()
    {
        canFlip = false;
        yield return new WaitForSeconds(flipCooldown);
        canFlip = true;
    }
}
