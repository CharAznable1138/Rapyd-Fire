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

    private GameObject enemy;
    private EnemyBehavior enemyBehavior;

    private GameObject player;
    private PlayerController playerController;

    private Vector2 movementVector;
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyBehavior = enemy.GetComponent<EnemyBehavior>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

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
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            playerController.enabled = false;
        }
    }
}
