using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Isolator : MonoBehaviour
{
    private GameObject[] clones;
    private void Start()
    {
        clones = GameObject.FindGameObjectsWithTag(tag);
        if (clones.Length > 0)
        {
            StartCoroutine("Isolate");
        }
    }

    private IEnumerator Isolate()
    {
        foreach (GameObject g in clones)
        {
            if (g != gameObject)
            {
                Destroy(g);
            }
        }
        yield return null;
    }
}
