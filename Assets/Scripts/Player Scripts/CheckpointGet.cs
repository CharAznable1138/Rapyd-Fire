using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointGet : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Amoun of time to display Checkpoint Get text. (Float)")]
    private float textDisplayTime = 2;

    [Tooltip("The SpawnPoint game object, which instantiates a new player object when the player clicks Retry.")]
    private GameObject spawnPoint;
    /// <summary>
    /// Check if the checkpoint has been claimed. True = yes, False = no. Readonly.
    /// </summary>
    internal bool IsClaimed { get; private set; }
    /// <summary>
    /// Amount of time to display Checkpoint Get text. (Float)
    /// </summary>
    internal float TextDisplayTime { get { return textDisplayTime; } }
    private void Start()
    {
        FindSpawnpointGameObject();
        SetIsClaimedToFalse();
    }
    /// <summary>
    /// Initialize "IsClaimed" boolean as false.
    /// </summary>
    private void SetIsClaimedToFalse()
    {
        IsClaimed = false;
    }

    /// <summary>
    /// Find the game object tagged "Spawnpoint".
    /// </summary>
    private void FindSpawnpointGameObject()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Spawnpoint");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartClaimCoroutineIfTouchingPlayer(collision);
    }
    /// <summary>
    /// Check if the checkpoint is touching the player. If so, start the Claim coroutine.
    /// </summary>
    /// <param name="collision">The collider that just touched the checkpoint.</param>
    private void StartClaimCoroutineIfTouchingPlayer(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spawnPoint.transform.position = gameObject.transform.position;
            StartCoroutine("Claim");
        }
    }

    /// <summary>
    /// Display Checkpoint Get text for a specified amount of time, and let other scripts know that the checkpoint has been claimed.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Claim()
    {
        IsClaimed = true;
        yield return new WaitForSeconds(textDisplayTime);
        IsClaimed = false;
        gameObject.SetActive(false);
    }

}
