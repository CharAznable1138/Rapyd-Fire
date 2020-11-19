using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowShieldTimer : MonoBehaviour
{
    [Tooltip("The Player's game object.")]
    private GameObject player;
    [Tooltip("The PlayerController script attached to the Player's game object.")]
    private PlayerController playerController;

    [Tooltip("The instantiated Shield Timer UI object.")]
    private GameObject createShieldTimer;

    [Tooltip("List of UI objects tagged \"Shield Timer\".")]
    private GameObject[] shieldTimers;

    [SerializeField]
    [Tooltip("The Shield Timer prefab to be instantiated.")]
    private GameObject shieldTimer;


    private void Update()
    {
        if(PlayerExists())
        {
            if(playerController.IsShielded && !ShieldTimerExists())
            {
                createShieldTimer = Instantiate(shieldTimer, gameObject.transform);
            }
            if(!playerController.IsShielded && ShieldTimerExists())
            {
                Destroy(createShieldTimer.gameObject);
            }
        }
    }
    /// <summary>
    /// Check if other Shield Timer UI objects exist.
    /// </summary>
    /// <returns></returns>
    private bool ShieldTimerExists()
    {
        shieldTimers = GameObject.FindGameObjectsWithTag("Shield Timer");
        if(shieldTimers.Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Check if the Player's game object exists, and assign the PlayerController if so.
    /// </summary>
    /// <returns></returns>
    private bool PlayerExists()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
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
