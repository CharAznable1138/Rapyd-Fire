using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

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
        FindScoreTracker();
        FindTMPTextComponent();
    }
    /// <summary>
    /// Find the score display's TMP Text component.
    /// </summary>
    private void FindTMPTextComponent()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    /// <summary>
    /// Find the score tracker game object and its attached score tracker script.
    /// </summary>
    private void FindScoreTracker()
    {
        scoreTrackerObject = GameObject.FindGameObjectWithTag("Score Tracker");
        scoreTrackerScript = scoreTrackerObject.GetComponent<ScoreTracker>();
    }

    private void Update()
    {
        SetScoreText();
    }
    /// <summary>
    /// Set the text to be displayed in the score display.
    /// </summary>
    private void SetScoreText()
    {
        switch(CurrentSceneIsFinalResults())
        {
            case false:
                scoreText.text = $"Score: {scoreTrackerScript.Score}";
                break;
            case true:
                scoreText.text = scoreTrackerScript.Score.ToString();
                break;
        }
    }
    /// <summary>
    /// Return true if the currently loaded scene is the Final Results screen, otherwise return false.
    /// </summary>
    /// <returns></returns>
    private bool CurrentSceneIsFinalResults()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings)
        {
            return true;
        }
        return false;
    }
}
