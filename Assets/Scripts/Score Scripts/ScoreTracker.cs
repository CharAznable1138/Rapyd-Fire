using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : Singleton<MonoBehaviour>
{
    [SerializeField]
    private float starterScore = 0;

    [SerializeField]
    private GameObject scoreTextObject;

    private GameObject canvas;

    private TMP_Text scoreTextComponent;

    private float score;
    private float scoreChange;
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
            Debug.Log($"Score just changed by {scoreChange} points.");
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
