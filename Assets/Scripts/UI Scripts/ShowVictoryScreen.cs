using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowVictoryScreen : MonoBehaviour
{
    private GameObject finishLine;
    private LevelComplete levelComplete;

    [SerializeField]
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
