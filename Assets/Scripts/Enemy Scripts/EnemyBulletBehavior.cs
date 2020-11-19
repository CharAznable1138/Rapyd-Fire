using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The speed at which enemy bullets travel. (Float)")]
    private float bulletSpeed = 50;

    [SerializeField]
    [Tooltip("Time to wait until enemy bullet destroys itself. (Float)")]
    private float selfDestructCountdown = 1;

    [Tooltip("Rigidbody2D component attached to the enemy bullet prefab.")]
    private Rigidbody2D rigidbody2D;

    [Tooltip("EnemyBehavior script attached to the enemy prefab.")]
    private EnemyBehavior enemyBehavior;

    [Tooltip("Direction in which enemy bullet will move. (Vector2)")]
    private Vector2 movementVector;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        enemyBehavior = GetComponentInParent<EnemyBehavior>();

        Shoot();

        StartCoroutine("SelfDestruct");
    }
    /// <summary>
    /// Creates an enemy bullet and fires it in a specified direction.
    /// </summary>
    private void Shoot()
    {
        if (enemyBehavior.AimingUp)
        {
            movementVector = new Vector2(rigidbody2D.velocity.x, bulletSpeed);
        }
        else
        {
            if (enemyBehavior.FacingRight)
            {
                movementVector = new Vector2(bulletSpeed, rigidbody2D.velocity.y);
            }
            else
            {
                movementVector = new Vector2(-bulletSpeed, rigidbody2D.velocity.y);
            }
        }
        rigidbody2D.velocity = movementVector;
    }
    /// <summary>
    /// Makes enemy bullet destroy itself.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(selfDestructCountdown);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("Bounds"))
        {
            Destroy(gameObject);
        }
    }
}
