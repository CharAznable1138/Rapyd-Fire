using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupCollect : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SelfDestructOnContactWithPlayer(collision);
    }
    /// <summary>
    /// Check if this powerup just touched the player. If so, destroy this powerup.
    /// </summary>
    /// <param name="collision">The collider that just touched this collectible.</param>
    private void SelfDestructOnContactWithPlayer(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
