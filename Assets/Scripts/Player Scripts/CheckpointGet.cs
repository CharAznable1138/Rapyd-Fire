using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointGet : MonoBehaviour
{
    [SerializeField]
    private float textDisplayTime = 2;

    private GameObject spawnPoint;
    internal bool IsClaimed { get; private set; }
    private void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Spawnpoint");
        IsClaimed = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            spawnPoint.transform.position = gameObject.transform.position;
            StartCoroutine("Claim");
        }
    }
    private IEnumerator Claim()
    {
        IsClaimed = true;
        yield return new WaitForSeconds(textDisplayTime);
        IsClaimed = false;
        gameObject.SetActive(false);
    }

}
