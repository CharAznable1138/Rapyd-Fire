using System.Collections;
using UnityEngine;

public class PlayerBulletBehavior : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Speed at which player bullets move. (Float)")]
    private float bulletSpeed = 50;

    [SerializeField]
    [Tooltip("Time that passes between when player bullet is fired and when bullet destroys itself. (Float)")]
    private float selfDestructCountdown = 1;

    [Tooltip("The Rigidbody2D component attached to the player bullet prefab.")]
    private Rigidbody2D rigidbody2D;

    [Tooltip("The PlayerController component attached to the player's game object.")]
    private PlayerController playerController;

    [Tooltip("Direction in which to fire player bullet. (Vector2)")]
    private Vector2 movementVector;

    [SerializeField]
    [Tooltip("The sound to play when the player fires a bullet.")]
    private AudioClip bulletSound;

    [Tooltip("The Sound Manager game object.")]
    private GameObject soundManagerObject;

    [Tooltip("The SoundManager script attached to the Sound Manager game object.")]
    private SoundManager soundManagerScript;

    private void Start()
    {
        AssignComponents();

        Shoot();

        StartCoroutine("SelfDestruct");
    }
    /// <summary>
    /// Set values for the components this script needs to communicate with.
    /// </summary>
    private void AssignComponents()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        playerController = GetComponentInParent<PlayerController>();

        soundManagerObject = GameObject.FindGameObjectWithTag("Sound Manager");

        soundManagerScript = soundManagerObject.GetComponent<SoundManager>();
    }

    /// <summary>
    /// Instantiate a player bullet then launch it in a specified direction.
    /// </summary>
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
        soundManagerScript.PlaySound(bulletSound);
        rigidbody2D.AddForce(movementVector, ForceMode2D.Impulse);
    }
    /// <summary>
    /// Make the instantiated player bullet destroy itself.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(selfDestructCountdown);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SelfDestructOnContact(collision);
    }
    /// <summary>
    /// Destroy the player bullet on contact with any other game object, except the player or the boundaries.
    /// </summary>
    /// <param name="collision">The collider that the player bullet just touched.</param>
    private void SelfDestructOnContact(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Bounds"))
        {
            Destroy(gameObject);
        }
    }
}
