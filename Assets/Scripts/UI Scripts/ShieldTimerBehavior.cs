using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class ShieldTimerBehavior : MonoBehaviour
{
    private double startingTime;
    private double timeRemaining;
    private double timePassed;
    private string timeRemainingString;
    private GameObject player;
    private PlayerController playerController;
    private TMP_Text timerText;

    [SerializeField]
    private int decimalPlaces = 2;

    private void Start()
    {
        timePassed = 0;
        timerText = GetComponent<TMP_Text>();
        if(PlayerExists())
        {
            startingTime = playerController.ShieldTime;
        }
    }

    private void Update()
    {
        if (timeRemaining >= 0)
        {
            timePassed += Time.deltaTime;
            timeRemaining = System.Math.Round((startingTime - timePassed), decimalPlaces);
            timeRemainingString = timeRemaining.ToString($"F{decimalPlaces}", CultureInfo.InvariantCulture);
            timerText.text = timeRemainingString;
        }
        else
        {
            if (playerController.ShieldExpiryFlashIsOn)
            {
                timerText.text = "<WARNING>";
            }
            else
            {
                timerText.text = null;
            }
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
