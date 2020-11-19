using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowShieldGetText : MonoBehaviour
{
    [Tooltip("The Player's game object.")]
    private GameObject player;
    [Tooltip("The PlayerController script attached to the Player's game object.")]
    private PlayerController playerController;

    [SerializeField]
    [Tooltip("The UI object that displays the Shield Get text.")]
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
    /// <summary>
    /// Check if the Player's game object exists, and assign the PlayerController if so.
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
