using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTextBehavior : MonoBehaviour
{
    [SerializeField]
    private float maximumHeight;

    [SerializeField]
    private float moveSpeed;

    private void Update()
    {
        transform.Translate(Vector3.up *moveSpeed * Time.deltaTime);
        if(transform.position.y > maximumHeight)
        {
            Destroy(gameObject);
        }
    }
}
