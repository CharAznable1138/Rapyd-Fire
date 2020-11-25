using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The amount of points to be subtracted from the Player's score if the Player is defeated. (Float)")]
    private float playerDeathDemerits = 1;

    [Tooltip("The empty game object which tracks the Player's score.")]
    private GameObject scoreTrackerObject;
    [Tooltip("The ScoreTracker script attached to the Score Tracker game object.")]
    private ScoreTracker scoreTrackerScript;

    [Tooltip("The PlayerController script attached to the Player's game object.")]
    private PlayerController playerController;

    [Tooltip("True = Player is dead, False = Player is not dead. (Bool)")]
    private bool playerIsDead;
    private void Start()
    {
        FindPlayerController();
        SetPlayerIsDeadToFalse();
        FindScoreTracker();
    }
    /// <summary>
    /// Set PlayerIsDead to its default value, false.
    /// </summary>
    private void SetPlayerIsDeadToFalse()
    {
        playerIsDead = false;
    }

    /// <summary>
    /// Find the Score Tracker game object and its attached Score Tracker script.
    /// </summary>
    private void FindScoreTracker()
    {
        scoreTrackerObject = GameObject.FindGameObjectWithTag("Score Tracker");
        scoreTrackerScript = scoreTrackerObject.GetComponent<ScoreTracker>();
    }

    /// <summary>
    /// Find the PlayerController component that this script needs to communicate with.
    /// </summary>
    private void FindPlayerController()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        KillPlayerOnContact(collision);
    }
    /// <summary>
    /// Check if the object the player just touched was an enemy bullet, a hazard, or the boundaries.
    /// If so, kill the player.
    /// </summary>
    /// <param name="collision">The collider that the player just touched.</param>
    private void KillPlayerOnContact(Collider2D collision)
    {
        if (((collision.gameObject.CompareTag("Enemy Bullet") || collision.gameObject.CompareTag("Hazard")) && !playerController.IsShielded) || collision.gameObject.CompareTag("Bounds"))
        {
            KillPlayer();
        }
    }

    /// <summary>
    /// Destroy the Player's game object and take away points from the Player's score accordingly. If the player's score is negative, set it to 0.
    /// </summary>
    private void KillPlayer()
    {
        if (!playerIsDead)
        {
            playerIsDead = true;
            scoreTrackerScript.Score -= playerDeathDemerits;
            if (scoreTrackerScript.Score < 0)
            {
                scoreTrackerScript.Score = 0;
            }
        }
        Destroy(gameObject);
    }
}
