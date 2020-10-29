using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    internal bool PlayerIsDead { get; private set; }
    private void Start()
    {
        PlayerIsDead = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy Bullet") || collision.gameObject.CompareTag("Bounds"))
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        PlayerIsDead = true;
        Destroy(gameObject);
    }
}
