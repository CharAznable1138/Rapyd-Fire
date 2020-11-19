using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class ShieldTimerBehavior : MonoBehaviour
{
    [Tooltip("The amount of time at which to start the shield timer. (Float)")]
    private double startingTime;
    [Tooltip("The amuont of time left before the shield timer expires. (Float)")]
    private double timeRemaining;
    [Tooltip("The amount of time that has passed since this object was instantiated. (Float)")]
    private double timePassed;
    [Tooltip("The amount of time left before the shield timer expires. (String)")]
    private string timeRemainingString;
    [Tooltip("The Player's game object.")]
    private GameObject player;
    [Tooltip("The PlayerController script attached to the Player's game object.")]
    private PlayerController playerController;
    [Tooltip("The TextMeshPro Text component attached to the this game object.")]
    private TMP_Text timerText;

    [SerializeField]
    [Tooltip("The amount of decimal places to which to round up remaining time. (Integer)")]
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
    /// <summary>
    /// Check if the Player's game object exists, and assign the PlayerController if it does.
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
