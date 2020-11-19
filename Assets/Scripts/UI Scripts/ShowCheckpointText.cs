using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShowCheckpointText : MonoBehaviour
{
    private GameObject[] checkpoints;
    private CheckpointGet checkpointGet;
    private GameObject player;

    [SerializeField]
    private GameObject checkpointText;

    private bool showTextCoroutineIsRunning;

    private void Start()
    {
        showTextCoroutineIsRunning = false;
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
    }
    private void Update()
    {
        if(checkpoints.Length > 0 && !showTextCoroutineIsRunning)
        {
            StopAllCoroutines();
            StartCoroutine(ShowText());
        }
        if(!PlayerExists() && checkpointText.activeSelf)
        {
            checkpointText.SetActive(false);
        }
    }

    private IEnumerator ShowText()
    {
        foreach (GameObject g in checkpoints)
        {
            checkpointGet = g.GetComponent<CheckpointGet>();
            if (checkpointGet.IsClaimed)
            {
                showTextCoroutineIsRunning = true;
                checkpointText.SetActive(true);
                yield return new WaitForSeconds(checkpointGet.TextDisplayTime);
            }
            checkpointText.SetActive(false);
            showTextCoroutineIsRunning = false;
        }
        yield break;
    }

    private bool PlayerExists()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
