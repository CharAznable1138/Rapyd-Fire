using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowShieldTimer : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;

    private GameObject createShieldTimer;

    private GameObject[] shieldTimers;

    [SerializeField]
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
