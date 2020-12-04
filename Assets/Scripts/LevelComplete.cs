using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    [Tooltip("List of Enemy game objects in scene.")]
    private GameObject[] enemies;
    [Tooltip("List of Enemy Bullet game objects in scene.")]
    private GameObject[] enemyBullets;
    /// <summary>
    /// True = level is complete, False = level is not complete. (Bool, Readonly)
    /// </summary>
    internal bool LevelIsComplete { get; private set; }
    private void Start()
    {
        SetLevelIsCompleteToFalse();
    }
    /// <summary>
    /// Set LevelIsComplete to its default value, false.
    /// </summary>
    private void SetLevelIsCompleteToFalse()
    {
        LevelIsComplete = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EndLevelIfPlayerTouchesFinishLine(collision);
    }
    /// <summary>
    /// Check if the player just touched the finish line.
    /// If so, start the Destroy All Enemies coroutine, end the level, then destroy the player.
    /// </summary>
    /// <param name="collision">The collider that just touched the finish line.</param>
    private void EndLevelIfPlayerTouchesFinishLine(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemyBullets = GameObject.FindGameObjectsWithTag("Enemy Bullet");
            StartCoroutine("DestroyAllEnemies");
            LevelIsComplete = true;
            Destroy(collision.gameObject);

        }
    }

    /// <summary>
    /// Destroy all Enemy and Enemy Bullet game objects.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DestroyAllEnemies()
    {
        foreach (GameObject g in enemies)
        {
            Destroy(g);
        }
        foreach (GameObject g in enemyBullets)
        {
            Destroy(g);
        }
        yield return null;
    }
}
