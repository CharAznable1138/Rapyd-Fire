using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    private float playerDeathDemerits = 1;

    private GameObject scoreTrackerObject;
    private ScoreTracker scoreTrackerScript;

    private PlayerController playerController;

    private bool playerIsDead;
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerIsDead = false;
        scoreTrackerObject = GameObject.FindGameObjectWithTag("Score Tracker");
        scoreTrackerScript = scoreTrackerObject.GetComponent<ScoreTracker>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((collision.gameObject.CompareTag("Enemy Bullet") || collision.gameObject.CompareTag("Hazard")) && !playerController.IsShielded) || collision.gameObject.CompareTag("Bounds"))
        {
            KillPlayer();
        }
    }

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
