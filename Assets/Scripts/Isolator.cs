using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Isolator : MonoBehaviour
{
    [Tooltip("List of other game objects with the same tag as this game object.")]
    private GameObject[] clones;
    private void Start()
    {
        clones = GameObject.FindGameObjectsWithTag(tag);
        if (clones.Length > 0)
        {
            StartCoroutine("Isolate");
        }
    }
    /// <summary>
    /// Destroy all game objects with this game object's tag, except for this game object.
    /// </summary>
    /// <returns></returns>
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
