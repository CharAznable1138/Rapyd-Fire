using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    private GameObject scoreTrackerObject;
    private ScoreTracker scoreTrackerScript;
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
