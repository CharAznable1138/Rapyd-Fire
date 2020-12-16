using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupCollect : MonoBehaviour
{
    private GameObject soundManagerObject;

    private SoundManager soundManagerScript;

    [SerializeField]
    private AudioClip powerupCollectSound;

    private void Start()
    {
        FindSoundManager();
    }
    private void FindSoundManager()
    {
        soundManagerObject = GameObject.FindGameObjectWithTag("Sound Manager");
        soundManagerScript = soundManagerObject.GetComponent<SoundManager>();
    }
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
            soundManagerScript.PlaySound(powerupCollectSound, 0.5f);
            Destroy(gameObject);
        }
    }
}
