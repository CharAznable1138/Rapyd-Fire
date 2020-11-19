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

    private void Start()
    {
        finishLine = GameObject.FindGameObjectWithTag("Finish Line");
        levelComplete = finishLine.GetComponent<LevelComplete>();
    }
    private void Update()
    {
        if (levelComplete.LevelIsComplete)
        {
            victoryPanel.SetActive(true);
        }
        else
        {
            victoryPanel.SetActive(false);
        }
    }
}
