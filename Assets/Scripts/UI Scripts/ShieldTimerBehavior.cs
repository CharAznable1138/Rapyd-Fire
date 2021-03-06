﻿using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class ShieldTimerBehavior : MonoBehaviour
{
    [Tooltip("The game object responsible for playing music.")]
    private GameObject musicManagerObject;
    [Tooltip("The script that lets the Music Manager game object play music.")]
    private MusicManager musicManagerScript;
    private AudioClip normalMusic;
    [Tooltip("The amount of time at which to start the shield timer. (Float)")]
    private double startingTime;
    [Tooltip("The amuont of time left before the shield timer expires. (Float)")]
    private double timeRemaining;
    [Tooltip("The amount of time that has passed since this object was instantiated. (Float)")]
    private double timePassed;
    [Tooltip("The amount of time left before the shield timer expires. (String)")]
    private string timeRemainingString;
    [Tooltip("The Player's game object.")]
    private GameObject player;
    [Tooltip("The PlayerController script attached to the Player's game object.")]
    private PlayerController playerController;
    [Tooltip("The TextMeshPro Text component attached to the this game object.")]
    private TMP_Text timerText;
    [Tooltip("The game object which, if touched by the Player, ends the level.")]
    private GameObject finishLine;
    [Tooltip("The script which lets the Finish Line game object end the level if touched by the Player.")]
    private LevelComplete levelComplete;

    [SerializeField]
    [Tooltip("The amount of decimal places to which to round up remaining time. (Integer)")]
    private int decimalPlaces = 2;

    [SerializeField]
    [Tooltip("The music to be played when the Player collects a powerup.")]
    private AudioClip powerupMusic;

    private void Start()
    {
        ResetTimePassed();
        FindTMPTextComponent();
        FindMusicManager();
        FindLevelComplete();
        SetNormalMusic();
        SetStartingTime();
        PlayPowerupMusic();
    }
    /// <summary>
    /// Get a reference to the Music Manager game object and its matching script.
    /// </summary>
    private void FindMusicManager()
    {
        musicManagerObject = GameObject.FindGameObjectWithTag("Music Manager");
        musicManagerScript = musicManagerObject.GetComponent<MusicManager>();
    }
    /// <summary>
    /// Get a reference to the Finish Line game object and its LevelComplete script.
    /// </summary>
    private void FindLevelComplete()
    {
        finishLine = GameObject.FindGameObjectWithTag("Finish Line");
        levelComplete = finishLine.GetComponent<LevelComplete>();
    }
    /// <summary>
    /// Get a reference to the current scene's default music.
    /// </summary>
    private void SetNormalMusic()
    {
        normalMusic = musicManagerScript.CurrentMusic();
    }
    /// <summary>
    /// If the player exists, set the starting time equal to the player's shield time.
    /// </summary>
    private void SetStartingTime()
    {
        if (PlayerExists())
        {
            startingTime = playerController.ShieldTime;
        }
    }
    /// <summary>
    /// Stop all music tracks, then play the Powerup music.
    /// </summary>
    private void PlayPowerupMusic()
    {
        musicManagerScript.StopMusic();
        musicManagerScript.PlayMusic(powerupMusic, 1.0f);
    }
    /// <summary>
    /// Stop all music tracks, then, if the level isn't complete, play the normal stage music.
    /// </summary>
    private void StopPowerupMusic()
    {
        musicManagerScript.StopMusic();
        if (!levelComplete.LevelIsComplete)
        {
            musicManagerScript.PlayMusic(normalMusic, 1.0f);
        }
    }
    /// <summary>
    /// Find the TMP Text component attached to this game object.
    /// </summary>
    private void FindTMPTextComponent()
    {
        timerText = GetComponent<TMP_Text>();
    }

    /// <summary>
    /// Set timePassed to its default value, 0.
    /// </summary>
    private void ResetTimePassed()
    {
        timePassed = 0;
    }

    private void Update()
    {
        SetTimerText();
    }
    /// <summary>
    /// If there's still time remaining, set the timer text accordingly.
    /// Otherwise, set the timer text to "WARNING" then flash it on and off as the player's colors flash.
    /// </summary>
    private void SetTimerText()
    {
        if (timeRemaining >= 0)
        {
            timePassed += Time.deltaTime;
            timeRemaining = System.Math.Round((startingTime - timePassed), decimalPlaces);
            timeRemainingString = timeRemaining.ToString($"F{decimalPlaces}", CultureInfo.InvariantCulture);
            timerText.text = timeRemainingString;
        }
        else
        {
            if (playerController.ShieldExpiryFlashIsOn)
            {
                timerText.text = "<WARNING>";
            }
            else
            {
                timerText.text = null;
            }
        }
    }

    private void OnDestroy()
    {
        StopPowerupMusic();
    }

    /// <summary>
    /// Check if the Player's game object exists, and assign the PlayerController if it does.
    /// </summary>
    /// <returns></returns>
    private bool PlayerExists()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            playerController = player.GetComponent<PlayerController>();
            return true;
        }
        else
        {
            return false;
        }
    }
}
