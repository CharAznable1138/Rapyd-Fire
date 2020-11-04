﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseText;

    private bool isPaused = false;

    private GameObject player;
    private PlayerDeath playerDeath;

    private void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            if (PlayerExists())
            {
                if (!playerDeath.PlayerIsDead)
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
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        pauseText.SetActive(true);
    }
    private void Unpause()
    {
        Time.timeScale = 1;
        pauseText.SetActive(false);
    }
    private bool PlayerExists()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerDeath = player.GetComponent<PlayerDeath>();
            return true;
        }
        else
        {
            return false;
        }
    }
}
