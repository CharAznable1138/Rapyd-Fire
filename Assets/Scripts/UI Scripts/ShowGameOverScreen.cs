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

    [Tooltip("The UI text that displays while the Player is shielded.")]
    private GameObject shieldTimer;

    private void Update()
    {
        ShowOrHideGameOverPanel();
    }
    /// <summary>
    /// Hide the Game Over panel unless the player exists, in which case
    /// show the Game Over panel unless the Victory panel is showing.
    /// </summary>
    private void ShowOrHideGameOverPanel()
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
                FindAndHideShieldTimer();
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

    /// <summary>
    /// Determine if there is currently a Shield Timer active. If so, hide it.
    /// </summary>
    private void FindAndHideShieldTimer()
    {
        shieldTimer = GameObject.FindGameObjectWithTag("Shield Timer");
        if (shieldTimer != null)
        {
            shieldTimer.SetActive(false);
        }
    }
}
