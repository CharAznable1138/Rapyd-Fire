﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private GameObject powerUpText;

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy Bullet"))
        {
            powerUpText.SetActive(false);
            gameOverPanel.SetActive(true);
            Destroy(gameObject);
        }
    }
}
