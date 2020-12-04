using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Isolator : MonoBehaviour
{
    [Tooltip("List of other game objects with the same tag as this game object.")]
    private GameObject[] clones;
    private void Start()
    {
        FindClones();
        RunIsolateCoroutine();
    }
    /// <summary>
    /// If clones are found in the scene, start the Isolate coroutine.
    /// </summary>
    private void RunIsolateCoroutine()
    {
        if (clones.Length > 0)
        {
            StartCoroutine("Isolate");
        }
    }

    /// <summary>
    /// Find all game objects in the scene with the same tag as this game object.
    /// </summary>
    private void FindClones()
    {
        clones = GameObject.FindGameObjectsWithTag(tag);
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
