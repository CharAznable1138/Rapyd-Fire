using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCollect : MonoBehaviour
{
    [SerializeField]
    private float selfDestructDelay = 0.1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
