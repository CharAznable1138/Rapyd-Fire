using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The amount of points to be awarded to the player if an enemy is defeated. (Float)")]
    private float enemyPoints = 10;

    [Tooltip("The empty game object which keeps track of the player's score.")]
    private GameObject scoreTrackerObject;

    [Tooltip("The ScoreTracker component attached to the Score Tracker object.")]
    private ScoreTracker scoreTrackerScript;

    [Tooltip("True = enemy is dead, False = enemy is not dead.")]
    private bool enemyIsDead = false;

    private void Start()
    {
        FindScoreTracker();
    }
    /// <summary>
    /// Assign values to the Score Tracker game object and its attached Score Tracker script.
    /// </summary>
    private void FindScoreTracker()
    {
        scoreTrackerObject = GameObject.FindGameObjectWithTag("Score Tracker");
        scoreTrackerScript = scoreTrackerObject.GetComponent<ScoreTracker>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        KillEnemyIfHitByPlayerBullet(collision);
    }
    /// <summary>
    /// Check if this enemy game object has just collided with a game object tagged "Player Bullet".
    /// If so, kill this enemy and increase the player's score accordingly.
    /// </summary>
    /// <param name="collision">The collider that this game object has just collided with.</param>
    private void KillEnemyIfHitByPlayerBullet(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player Bullet"))
        {
            if (!enemyIsDead)
            {
                scoreTrackerScript.Score += enemyPoints;
                enemyIsDead = true;
            }
            Destroy(gameObject);
        }
    }
}
