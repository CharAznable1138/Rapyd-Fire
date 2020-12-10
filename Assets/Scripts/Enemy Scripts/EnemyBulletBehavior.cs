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

    [SerializeField]
    [Tooltip("The sound to play when the enemy fires a bullet.")]
    private AudioClip bulletSound;

    [Tooltip("The Sound Manager game object.")]
    private GameObject soundManagerObject;

    [Tooltip("The SoundManager script attached to the Sound Manager game object.")]
    private SoundManager soundManagerScript;

    private void Start()
    {
        AssignScripts();

        Shoot();

        StartCoroutine(SelfDestruct());
    }
    /// <summary>
    /// Get values for the scripts this script needs to communicate with.
    /// </summary>
    private void AssignScripts()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        enemyBehavior = GetComponentInParent<EnemyBehavior>();

        soundManagerObject = GameObject.FindGameObjectWithTag("Sound Manager");

        soundManagerScript = soundManagerObject.GetComponent<SoundManager>();
    }

    /// <summary>
    /// Creates an enemy bullet and fires it in a specified direction.
    /// </summary>
    private void Shoot()
    {
        switch (enemyBehavior.EnemyTypeIsFlier)
        {
            case false:
                if (enemyBehavior.FacingRight)
                {
                    movementVector = new Vector2(bulletSpeed, rigidbody2D.velocity.y);
                }
                else
                {
                    movementVector = new Vector2(-bulletSpeed, rigidbody2D.velocity.y);
                }
                break;
            case true:
                if (enemyBehavior.FacingRight)
                {
                    movementVector = new Vector2(bulletSpeed, -bulletSpeed);
                }
                else
                {
                    movementVector = new Vector2(-bulletSpeed, -bulletSpeed);
                }
                break;
        }
        soundManagerScript.PlaySoundLowPitch(bulletSound);
        rigidbody2D.AddForce(movementVector, ForceMode2D.Impulse);

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
        DestroyOnContact(collision);
    }
    /// <summary>
    /// Destroy the game object on contact with any other game object, except for those tagged "Enemy" or "Bounds".
    /// </summary>
    /// <param name="collision">The collider that this game object has just collided with.</param>
    private void DestroyOnContact(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("Bounds"))
        {
            Destroy(gameObject);
        }
    }
}
