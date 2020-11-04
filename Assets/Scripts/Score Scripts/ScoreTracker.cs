using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : Singleton<MonoBehaviour>
{
    [SerializeField]
    private float starterScore = 0;

    private float score;
    internal float Score { get { return score; } set { score = value; } }

    private void Start()
    {
        score = starterScore;
    }
}
