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
        scoreTrackerObject = GameObject.FindGameObjectWithTag("Score Tracker");
        scoreTrackerScript = scoreTrackerObject.GetComponent<ScoreTracker>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
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
