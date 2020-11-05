using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGameOverScreen : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
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
