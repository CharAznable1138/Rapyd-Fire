using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : Singleton<MonoBehaviour>
{
    [SerializeField]
    private float starterScore = 0;

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
        score = starterScore;
    }
    private void Update()
    {
        if(score != 0 && scoreChange != 0)
        {
            Debug.Log($"Score just changed by {scoreChange} points.");
            scoreChange = 0;
        }
    }
}
