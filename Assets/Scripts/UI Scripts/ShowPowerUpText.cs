﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPowerUpText : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;

    [SerializeField]
    private GameObject powerupText;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }
    private void Update()
    {
        if(playerController.IsPoweredUp)
        {
            powerupText.SetActive(true);
        }
        else
        {
            powerupText.SetActive(false);
        }
    }
}