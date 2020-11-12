using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTextBehavior : MonoBehaviour
{
    [SerializeField]
    private float maximumHeight;

    [SerializeField]
    private float minimumHeight;

    [SerializeField]
    private float moveSpeed;

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
