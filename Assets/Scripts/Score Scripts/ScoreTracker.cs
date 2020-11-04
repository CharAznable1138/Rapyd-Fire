using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : Singleton<MonoBehaviour>
{
    private float score;
    internal float Score { get { return score; } set { score = value; } }
}
