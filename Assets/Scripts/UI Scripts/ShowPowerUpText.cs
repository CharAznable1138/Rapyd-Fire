using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPowerUpText : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;

    [SerializeField]
    private GameObject powerupText;

    private void Update()
    {
        if (PlayerExists())
        {
            if (playerController.IsPoweredUp)
            {
                powerupText.SetActive(true);
            }
            else
            {
                powerupText.SetActive(false);
            }
        }
        else
        {
            powerupText.SetActive(false);
        }
    }
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
