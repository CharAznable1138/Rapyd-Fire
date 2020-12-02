using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The prefab containing all the powerups in the level.")]
    private GameObject powerupSet;

    /// <summary>
    /// Respawn all the powerups in the level.
    /// </summary>
    internal void SpawnPowerups()
    {
        Instantiate(powerupSet, powerupSet.transform.position, powerupSet.transform.rotation);
    }
}
