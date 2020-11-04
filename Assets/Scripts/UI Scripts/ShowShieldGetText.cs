using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowShieldGetText : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;

    [SerializeField]
    private GameObject shieldGetText;

    private void Update()
    {
        if (PlayerExists())
        {
            if (playerController.IsShielded)
            {
                shieldGetText.SetActive(true);
            }
            else
            {
                shieldGetText.SetActive(false);
            }
        }
        else
        {
            shieldGetText.SetActive(false);
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
