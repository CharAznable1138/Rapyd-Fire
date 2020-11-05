using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawSpin : MonoBehaviour
{
    [SerializeField]
    private float spinningSpeed;

    private void Update()
    {
        gameObject.transform.Rotate(new Vector3(0, 0, spinningSpeed));
    }
}
