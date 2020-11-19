using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The UI object that displays when the game is paused.")]
    private GameObject pauseText;

    [Tooltip("True = Game is paused, False = Game is not paused. (Bool)")]
    private bool isPaused = false;

    [Tooltip("The Player's game object.")]
    private GameObject player;

    private void Update()
    {
        if (Input.GetKeyDown("escape") && PlayerExists())
        {
            if (isPaused)
            {
                Unpause();
                isPaused = false;
            }
            else
            {
                Pause();
                isPaused = true;
            }
        }
    }
    /// <summary>
    /// Pause the game by setting the time scale to 0, display pause text.
    /// </summary>
    private void Pause()
    {
        Time.timeScale = 0;
        pauseText.SetActive(true);
    }
    /// <summary>
    /// Resume the game by setting the time scale to 1, hide pause text.
    /// </summary>
    private void Unpause()
    {
        Time.timeScale = 1;
        pauseText.SetActive(false);
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
