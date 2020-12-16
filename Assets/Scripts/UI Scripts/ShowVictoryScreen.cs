using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowVictoryScreen : MonoBehaviour
{
    [Tooltip("The game object that causes the Player to win the level if it touches the Player.")]
    private GameObject finishLine;
    [Tooltip("The Level Complete component attached to the Finish Line game object.")]
    private LevelComplete levelComplete;

    [SerializeField]
    [Tooltip("The UI panel that displays if the Player completes the level.")]
    private GameObject victoryPanel;

    [Tooltip("The UI text that displays while the Player is shielded.")]
    private GameObject shieldTimer;

    private void Start()
    {
        FindFinishLine();
        FindLevelCompleteComponent();
    }
    /// <summary>
    /// Find the Level Complete component attached to the finish line in the scene.
    /// </summary>
    private void FindLevelCompleteComponent()
    {
        levelComplete = finishLine.GetComponent<LevelComplete>();
    }

    /// <summary>
    /// Find the finish line in the scene.
    /// </summary>
    private void FindFinishLine()
    {
        finishLine = GameObject.FindGameObjectWithTag("Finish Line");
    }

    private void Update()
    {
        ShowOrHideVictoryPanel();
    }
    /// <summary>
    /// If the level is completed, show the victory panel.
    /// Otherwise, hide the victory panel.
    /// </summary>
    private void ShowOrHideVictoryPanel()
    {
        if (levelComplete.LevelIsComplete)
        {
            victoryPanel.SetActive(true);
            FindAndHideShieldTimer();
        }
        else
        {
            victoryPanel.SetActive(false);
        }
    }
    /// <summary>
    /// Determine if there is currently a Shield Timer active. If so, hide it.
    /// </summary>
    private void FindAndHideShieldTimer()
    {
        shieldTimer = GameObject.FindGameObjectWithTag("Shield Timer");
        if(shieldTimer != null)
        {
            shieldTimer.SetActive(false);
        }
    }
}
