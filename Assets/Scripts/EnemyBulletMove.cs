using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMove : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 50;

    [SerializeField]
    private float selfDestructCountdown = 1;

    private Rigidbody2D rigidbody2D;

    private EnemyBehavior enemyBehavior;

    private Vector2 movementVector;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        enemyBehavior = GetComponentInParent<EnemyBehavior>();

        Shoot();

        StartCoroutine("SelfDestruct");
    }
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
