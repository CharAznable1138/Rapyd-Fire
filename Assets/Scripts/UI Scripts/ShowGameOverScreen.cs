using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGameOverScreen : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private GameObject gameOverPanel;

    private void Update()
    {
        if (PlayerExists())
        {
            gameOverPanel.SetActive(false);
        }
        else
        {
            gameOverPanel.SetActive(true);
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
