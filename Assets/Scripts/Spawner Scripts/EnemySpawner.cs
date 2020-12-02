using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The prefab containing all the enemies in the level.")]
    private GameObject enemySet;

    /// <summary>
    /// Respawn all the enemies in the level.
    /// </summary>
    internal void SpawnEnemies()
    {
        Instantiate(enemySet, enemySet.transform.position, enemySet.transform.rotation);
    }
}
