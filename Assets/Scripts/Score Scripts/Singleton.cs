using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generic that inherits from MonoBehavior ensures that we can use the Unity API.
// Where T is MonoBehaviour (or inherits from MonoBehaviour) ensures T to Singleton<T>.
public class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    static public T Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
