using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShowCheckpointText : MonoBehaviour
{
    [Tooltip("List of game objects tagged \"Checkpoint\".")]
    private GameObject[] checkpoints;
    [Tooltip("The CheckpointGet script attached to the Checkpoint game object.")]
    private CheckpointGet checkpointGet;
    [Tooltip("The Player's game object.")]
    private GameObject player;

    [SerializeField]
    [Tooltip("The UI object which displays the Checkpoint Get text.")]
    private GameObject checkpointText;

    [Tooltip("True = Show Text coroutine is currently running, False = Show Text coroutine is not currently running. (Bool)")]
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
    /// <summary>
    /// Show Checkpoint Get text to the player when a Checkpoint is claimed, and let the script know that this coroutine is running.
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Check if the Player's game object exists. True = yes, False = no.
    /// </summary>
    /// <returns></returns>
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
