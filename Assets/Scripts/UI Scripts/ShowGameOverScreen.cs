using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGameOverScreen : MonoBehaviour
{
    [Tooltip("The Player's game object.")]
    private GameObject player;

    [SerializeField]
    [Tooltip("The UI panel that displays if the Player is defeated.")]
    private GameObject gameOverPanel;

    [SerializeField]
    [Tooltip("The UI panel that displays if the Player completes the level.")]
    private GameObject victoryPanel;

    private void Update()
    {
        if (PlayerExists())
        {
            gameOverPanel.SetActive(false);
        }
        else
        {
            if (victoryPanel.activeSelf == false)
            {
                gameOverPanel.SetActive(true);
            }
            else
            {
                gameOverPanel.SetActive(false);
            }
        }
    }
    /// <summary>
    /// Check if the Player's game object exists. True = yes, False = no.
    /// </summary>
    /// <returns></returns>
    private bool PlayerExists()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
