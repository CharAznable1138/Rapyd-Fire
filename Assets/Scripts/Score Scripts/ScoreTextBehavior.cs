using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTextBehavior : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The maximum height Score Text can reach before it is destroyed. (Float)")]
    private float maximumHeight;

    [SerializeField]
    [Tooltip("The minimum height Score Text can reach before it is destroyed. (Float)")]
    private float minimumHeight;

    [SerializeField]
    [Tooltip("Speed at which Score Text goes up or down.")]
    private float moveSpeed;

    [Tooltip("The TextMeshPro Text component attached to the Score Text game object.")]
    private TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (text.text.Contains("+"))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        else if (text.text.Contains("-"))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
        if(transform.position.y > maximumHeight || transform.position.y < minimumHeight)
        {
            Destroy(gameObject);
        }
    }
}
