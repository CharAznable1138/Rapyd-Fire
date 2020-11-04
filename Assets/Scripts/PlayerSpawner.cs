using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private GameObject HUD;

    [SerializeField]
    private GameObject mainCamera;

    private void Start()
    {
        SpawnPlayer();
    }

    internal void SpawnPlayer()
    {
        Instantiate(player, gameObject.transform.position, player.transform.rotation);
        var createHUD = Instantiate(HUD, canvas.transform.position, canvas.transform.rotation);
        createHUD.transform.SetParent(canvas.transform);
        Instantiate(mainCamera);
    }
}
