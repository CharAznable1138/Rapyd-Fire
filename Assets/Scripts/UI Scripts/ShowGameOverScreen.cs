using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGameOverScreen : MonoBehaviour
{
    private GameObject player;
    private PlayerDeath playerDeath;

    [SerializeField]
    private GameObject gameOverPanel;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerDeath = player.GetComponent<PlayerDeath>();
        }
    }
    private void Update()
    {
        if (playerDeath.PlayerIsDead)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            gameOverPanel.SetActive(false);
        }
    }
}
