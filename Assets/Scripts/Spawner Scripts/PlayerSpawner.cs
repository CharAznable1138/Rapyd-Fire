using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Player's game object.")]
    private GameObject player;

    [Tooltip("The Canvas containing all UI objects in the scene.")]
    private GameObject canvas;

    [SerializeField]
    [Tooltip("The UI panel which displays an array of relevant information the player unless the player is defeated or the level is completed.")]
    private GameObject HUD;

    private void Start()
    {
        FindCanvas();
        SpawnPlayer();
    }
    private void FindCanvas()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }
    /// <summary>
    /// Create a new Player game object and a new HUD UI object.
    /// </summary>
    internal void SpawnPlayer()
    {
        Instantiate(player, gameObject.transform.position, player.transform.rotation);
        var createHUD = Instantiate(HUD, canvas.transform.position, canvas.transform.rotation);
        createHUD.transform.SetParent(canvas.transform);
    }
}
