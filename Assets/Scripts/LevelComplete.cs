using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    private GameObject[] enemies;
    private GameObject[] enemyBullets;
    internal bool LevelIsComplete { get; private set; }
    private void Start()
    {
        LevelIsComplete = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemyBullets = GameObject.FindGameObjectsWithTag("Enemy Bullet");
            StartCoroutine("DestroyAllEnemies");
            LevelIsComplete = true;
            Destroy(collision.gameObject);

        }
    }
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
