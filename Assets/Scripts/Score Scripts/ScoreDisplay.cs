using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [Tooltip("The empty game object which tracks the Player's score.")]
    private GameObject scoreTrackerObject;
    [Tooltip("The ScoreTracker script attached to the Score Tracker game object.")]
    private ScoreTracker scoreTrackerScript;
    [Tooltip("The TextMeshPro Text component attached to the Score Display UI object.")]
    private TMP_Text scoreText;

    private void Start()
    {
        scoreTrackerObject = GameObject.FindGameObjectWithTag("Score Tracker");
        scoreTrackerScript = scoreTrackerObject.GetComponent<ScoreTracker>();
        scoreText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        scoreText.text = $"Score: {scoreTrackerScript.Score}";
    }
}
