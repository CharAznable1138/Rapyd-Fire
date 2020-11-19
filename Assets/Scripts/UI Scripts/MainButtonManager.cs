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
    /// Destroy the Score Tracker and return to the Main Menu.
    /// </summary>
    public void QuitToTitle()
    {
        scoreTracker = GameObject.FindGameObjectWithTag("Score Tracker");
        Destroy(scoreTracker);
        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// Respawn the player.
    /// </summary>
    public void Retry()
    {
        SpawnPlayer();
    }
    /// <summary>
    /// Load the next scene in the Build hierarchy.
    /// </summary>
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
}
