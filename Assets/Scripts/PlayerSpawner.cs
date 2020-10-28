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
    private GameObject mainCamera;

    private void Start()
    {
        SpawnPlayer();
    }

    internal void SpawnPlayer()
    {
        Instantiate(player, gameObject.transform);
        Instantiate(canvas);
        Instantiate(mainCamera);
    }
}
