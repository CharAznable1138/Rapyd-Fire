using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The speed at which enemies move.")]
    private float movementSpeed = 10;

    [SerializeField]
    [Tooltip("Distance to check if enemy is on the ground.")]
    private float groundCheckDistance = 1;

    [SerializeField]
    [Tooltip("Distance to check for other enemies.")]
    private float enemyContactCheckDistance = 1;

    [SerializeField]
    [Tooltip("Time after shooting before enemy can shoot again.")]
    private float cooldownTime = 0.5f;

    [SerializeField]
    [Tooltip("Time between when enemy detects player and when enemy can start shooting.")]
    private float initialDelayTime = 0.1f;

    [SerializeField]
    [Tooltip("Distance at which enemy can detect player.")]
    private float firingRange = 10;

    [SerializeField]
    [Tooltip("Bullet prefab to be fired.")]
    private GameObject bulletPrefab;

    [SerializeField]
    [Tooltip("Layer on which to check for the ground object.")]
    private LayerMask groundLayer;

    [SerializeField]
    [Tooltip("Layer on which to check for the player object.")]
    private LayerMask playerLayer;

    [SerializeField]
    [Tooltip("Layer on which to check for other enemies.")]
    private LayerMask enemyLayer;

    [Tooltip("True = enemy can shoot, False = enemy can't shoot.")]
    private bool canShoot = true;
    /// <summary>
    /// True = enemy is facing right, False = enemy is facing left
    /// </summary>
    internal bool FacingRight { get; private set; }
    /// <summary>
    /// True = enemy is aiming upward, False = enemy is not aiming upward
    /// </summary>
    internal bool AimingUp { get; private set; }

    [Tooltip("The Rigidbody2D component attached to the Enemy prefab.")]
    private Rigidbody2D rigidbody2D;
    [Tooltip("The SpriteRenderer component attached to the Enemy prefab.")]
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        FacingRight = true;
        AimingUp = false;
    }
    private void Update()
    {
        Move();
        if(canShoot && CanSeePlayer())
        {
            StartCoroutine("Shoot");
        }
    }
    /// <summary>
    /// Check if the enemy is on the ground. True = yes, False = no.
    /// </summary>
    /// <returns></returns>
    private bool IsOnGround()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = groundCheckDistance;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;

    }
    /// <summary>
    /// Check if the enemy can see the player. True = yes, False = no.
    /// </summary>
    /// <returns></returns>
    private bool CanSeePlayer()
    {
        Vector2 position = transform.position;
        Vector2 direction;
        float distance = firingRange;
        if(FacingRight)
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
    /// Check if the enemy is on the edge of its current platform. True = yes, False = no.
    /// </summary>
    /// <returns></returns>
    private bool IsOnEdge()
    {
        Vector2 position = transform.position;
        Vector2 direction;
        if(FacingRight)
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
        if(hit.collider != null)
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
    /// </summary>
    private void Move()
    {
        if(!IsOnEdge() && !IsTouchingOtherEnemy())
        {
            if (FacingRight)
            {
                rigidbody2D.velocity = new Vector2(movementSpeed, rigidbody2D.velocity.y);
            }
            else
            {
                rigidbody2D.velocity = new Vector2(-movementSpeed, rigidbody2D.velocity.y);
            }
        }
        else
        {
            Flip();
        }
    }
    /// <summary>
    /// Change which direction the enemy is facing.
    /// </summary>
    private void Flip()
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
    private void Aim()
    {
        //TODO: Implement logic to allow enemy to shoot player if player is detected above enemy
    }
}
