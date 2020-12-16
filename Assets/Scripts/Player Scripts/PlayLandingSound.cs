using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLandingSound : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The sound to be played when the player lands from a jump or fall.")]
    private AudioClip landingSound;

    [Tooltip("The game object responsible for playing sounds.")]
    private GameObject soundManagerObject;

    [Tooltip("The script that lets the Sound Manager game object play sounds.")]
    private SoundManager soundManagerScript;

    private void Start()
    {
        FindSoundManager();
    }
    /// <summary>
    /// Get references to the Sound Manager game object and its matching script.
    /// </summary>
    private void FindSoundManager()
    {
        soundManagerObject = GameObject.FindGameObjectWithTag("Sound Manager");
        soundManagerScript = soundManagerObject.GetComponent<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayLandingSoundIfTouchingGround(collision);
    }
    /// <summary>
    /// Check whether the player has landed on the ground or atop an enemy, and play the landing sound if so.
    /// </summary>
    /// <param name="_collision">The game object that the player's feet just touched.</param>
    private void PlayLandingSoundIfTouchingGround(Collider2D _collision)
    {
        if (_collision.CompareTag("Ground") || _collision.CompareTag("Enemy"))
        {
            soundManagerScript.PlaySound(landingSound, 0.5f);
        }
    }
}
