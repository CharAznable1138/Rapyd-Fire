using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10;

    [SerializeField]
    private float groundCheckDistance = 1;

    [SerializeField]
    private float enemyContactCheckDistance = 1;

    [SerializeField]
    private float cooldownTime = 0.5f;

    [SerializeField]
    private float initialDelayTime = 0.1f;

    [SerializeField]
    private float firingRange = 10;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private LayerMask playerLayer;

    [SerializeField]
    private LayerMask enemyLayer;

    private bool canShoot = true;
    internal bool FacingRight { get; private set; }
    internal bool AimingUp { get; private set; }

    private Rigidbody2D rigidbody2D;
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

    private IEnumerator Shoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(initialDelayTime);
        Instantiate(bulletPrefab, gameObject.transform);
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }

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
