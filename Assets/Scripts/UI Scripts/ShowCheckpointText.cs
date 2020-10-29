using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCheckpointText : MonoBehaviour
{
    private GameObject checkpoint;
    private CheckpointGet checkpointGet;

    [SerializeField]
    private GameObject checkpointText;

    private void Start()
    {
        checkpoint = GameObject.FindGameObjectWithTag("Checkpoint");
        if (checkpoint != null)
        {
            checkpointGet = checkpoint.GetComponent<CheckpointGet>();
        }
    }
    private void Update()
    {
        if (checkpoint != null)
        {
            if (checkpointGet.IsClaimed)
            {
                checkpointText.SetActive(true);
            }
            else
            {
                checkpointText.SetActive(false);
            }
        }
    }
}
