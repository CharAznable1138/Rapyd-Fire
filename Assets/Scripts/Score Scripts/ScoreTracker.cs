using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : Singleton<MonoBehaviour>
{
    [SerializeField]
    [Tooltip("The amount of points the Player will start with. (Float)")]
    private float starterScore = 0;

    [SerializeField]
    [Tooltip("The UI object which shows how many points the player has just gained or lost.")]
    private GameObject scoreTextObject;

    [SerializeField]
    [Tooltip("The Canvas on which all UI objects display.")]
    private GameObject canvas;
    
    [Tooltip("The TextMeshPro Text component attached to the Score Text UI object.")]
    private TMP_Text scoreTextComponent;

    [Tooltip("The Player's current score. (Float)")]
    private float score;
    [Tooltip("The amount of points the Player has just gained or lost. (Float)")]
    private float scoreChange;
    /// <summary>
    /// Check the player's current score, or, if changing the player's score, save the amount of points gained or lost.
    /// </summary>
    internal float Score 
    { 
        get { return score; } 
        set 
        {
            scoreChange = value - score;
            score = value;
        } 
    }

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        score = starterScore;
    }
    private void Update()
    {
        if(score != 0 && scoreChange != 0)
        {
            var scoreTextInstance = Instantiate(scoreTextObject, canvas.transform.position, canvas.transform.rotation);
            scoreTextInstance.transform.SetParent(canvas.transform);
            scoreTextComponent = scoreTextInstance.GetComponent<TMP_Text>();
            if (scoreChange > 0)
            {
                scoreTextComponent.text = $"+{scoreChange} points!";
            }
            if (scoreChange < 0)
            {
                scoreTextComponent.text = $"{scoreChange} points";
            }
            scoreChange = 0;
        }
    }
}
