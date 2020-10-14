using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 50;

    [SerializeField]
    private float selfDestructCountdown = 1;

    private Rigidbody2D rigidbody2D;

    private GameObject player;
    private PlayerController playerController;

    private Vector2 movementVector;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        Shoot();

        StartCoroutine("SelfDestruct");
    }

    private void Shoot()
    {
        if(playerController.AimingUp)
        {
            movementVector = new Vector2(rigidbody2D.velocity.x, bulletSpeed);
        }
        else if(playerController.AimingDown)
        {
            movementVector = new Vector2(rigidbody2D.velocity.x, -bulletSpeed);
        }
        else
        {
            if (playerController.FacingRight)
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
}
