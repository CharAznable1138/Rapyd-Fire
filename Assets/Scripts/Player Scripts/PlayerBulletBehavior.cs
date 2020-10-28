using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerBulletBehavior : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 50;

    [SerializeField]
    private float selfDestructCountdown = 1;

    private Rigidbody2D rigidbody2D;

    private PlayerController playerController;

    private Vector2 movementVector;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        playerController = GetComponentInParent<PlayerController>();

        Shoot();

        StartCoroutine("SelfDestruct");
    }

    private void Shoot()
    {
        switch(playerController.AimingDirection)
        {
            case PlayerController.AimingDirectionState.Up:
                movementVector = new Vector2(rigidbody2D.velocity.x, bulletSpeed);
                break;
            case PlayerController.AimingDirectionState.Down:
                movementVector = new Vector2(rigidbody2D.velocity.x, -bulletSpeed);
                break;
            case PlayerController.AimingDirectionState.Left:
                movementVector = new Vector2(-bulletSpeed, rigidbody2D.velocity.y);
                break;
            case PlayerController.AimingDirectionState.Right:
                movementVector = new Vector2(bulletSpeed, rigidbody2D.velocity.y);
                break;
            case PlayerController.AimingDirectionState.UpLeft:
                movementVector = new Vector2(-bulletSpeed, bulletSpeed);
                break;
            case PlayerController.AimingDirectionState.UpRight:
                movementVector = new Vector2(bulletSpeed, bulletSpeed);
                break;
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
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Bounds"))
        {
            Destroy(gameObject);
        }
    }
}
