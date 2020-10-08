using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 50;
    [SerializeField] private float selfDestructCountdown = 1;

    private Rigidbody2D rigidbody2D;

    private GameObject player;
    private PlayerController playerController;

    private float movementDirection;

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
        if(playerController.FacingRight)
        {
            movementDirection = 1;
        }
        else
        {
            movementDirection = -1;
        }
        rigidbody2D.velocity = new Vector2(movementDirection * bulletSpeed, rigidbody2D.velocity.y);
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(selfDestructCountdown);
        Destroy(gameObject);
    }
}
