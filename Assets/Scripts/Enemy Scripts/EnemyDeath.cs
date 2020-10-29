using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField]
    private float enemyPoints = 10;

    private GameObject scoreTrackerObject;
    private ScoreTracker scoreTrackerScript;
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
