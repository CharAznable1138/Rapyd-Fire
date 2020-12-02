using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtonManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Player's game object.")]
    private GameObject player;

    [Tooltip("The game object which instantiates a new player when the level starts, and when the Player clicks Retry.")]
    private GameObject spawnPoint;

    [Tooltip("The PlayerSpawner script attached to the Spawn Point game object.")]
    private PlayerSpawner playerSpawner;

    [Tooltip("The game object that handles spawning objects other than the player.")]
    private GameObject spawnManager;

    [Tooltip("The EnemySpawner script attached to the Spawn Manager game object.")]
    private EnemySpawner enemySpawner;

    [Tooltip("The PowerupSpawner script attached to the Spawn Manager game object.")]
    private PowerupSpawner powerupSpawner;

    [Tooltip("The empty game object which keeps track of the Player's score.")]
    private GameObject scoreTracker;
    /// <summary>
    /// If the game is running in the Unity Editor, exit Play mode and return to Edit mode. Otherwise, close the application entirely.
    /// </summary>
    public void QuitToDesktop()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    /// <summary>
    /// Return to the Main Menu.
    /// </summary>
    public void QuitToTitle()
    {
        DestroyScoreTracker();
        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// Respawn the player.
    /// </summary>
    public void Retry()
    {
        SpawnPlayer();
        SpawnEnemies();
        SpawnPowerups();
    }
    /// <summary>
    /// Load the next scene in the Build hierarchy.
    /// </summary>
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    /// <summary>
    /// Return to Level 1.
    /// </summary>
    public void RestartAtLevel1()
    {
        DestroyScoreTracker();
        SceneManager.LoadScene(1);
    }
    /// <summary>
    /// Instantiate a new Player game object at the current location of the Spawnpoint game object.
    /// </summary>
    private void SpawnPlayer()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Spawnpoint");
        playerSpawner = spawnPoint.GetComponent<PlayerSpawner>();
        playerSpawner.SpawnPlayer();
    }
    /// <summary>
    /// Respawn all enemies in the level.
    /// </summary>
    private void SpawnEnemies()
    {
        FindSpawnManager();
        enemySpawner = spawnManager.GetComponent<EnemySpawner>();
        enemySpawner.SpawnEnemies();
    }
    /// <summary>
    /// Respawn all powerups in the level.
    /// </summary>
    private void SpawnPowerups()
    {
        FindSpawnManager();
        powerupSpawner = spawnManager.GetComponent<PowerupSpawner>();
        powerupSpawner.SpawnPowerups();
    }
    /// <summary>
    /// Find the game object tagged "Spawn Manager" and assign it to the spawnManager variable.
    /// </summary>
    private void FindSpawnManager()
    {
        spawnManager = GameObject.FindGameObjectWithTag("Spawn Manager");
    }
    /// <summary>
    /// Find the Score Tracker game object and, if it exists, destroy it.
    /// </summary>
    private void DestroyScoreTracker()
    {
        scoreTracker = GameObject.FindGameObjectWithTag("Score Tracker");
        if (scoreTracker != null)
        {
            Destroy(scoreTracker);
        }
    }
}
