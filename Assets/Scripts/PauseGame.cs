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

    [SerializeField]
    private AudioClip pauseSound;

    private GameObject musicManagerObject;
    private MusicManager musicManagerScript;
    private GameObject soundManagerObject;
    private SoundManager soundManagerScript;

    private void Start()
    {
        FindSoundAndMusicManagers();
    }
    private void Update()
    {
        HandlePauseInput();
    }
    private void FindSoundAndMusicManagers()
    {
        musicManagerObject = GameObject.FindGameObjectWithTag("Music Manager");
        musicManagerScript = musicManagerObject.GetComponent<MusicManager>();
        soundManagerObject = GameObject.FindGameObjectWithTag("Sound Manager");
        soundManagerScript = soundManagerObject.GetComponent<SoundManager>();
    }
    /// <summary>
    /// If player presses the Escape key and isn't dead,
    /// Unpause the game if it is paused,
    /// otherwise Pause the game.
    /// </summary>
    private void HandlePauseInput()
    {
        if (Input.GetKeyDown("escape") && PlayerExists())
        {
            if (isPaused)
            {
                Unpause();
                ResumeMusic();
                isPaused = false;
            }
            else
            {
                Pause();
                PauseMusic();
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
    private void PauseMusic()
    {
        musicManagerScript.PauseMusic();
        soundManagerScript.PlaySound(pauseSound, 1.0f);
    }
    private void ResumeMusic()
    {
        soundManagerScript.PlaySound(pauseSound, 1.0f);
        musicManagerScript.PlayCurrentMusic();
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
