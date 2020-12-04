using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [Tooltip("The player's game object.")]
    private GameObject player;

    private void Update()
    {
        SetCameraPosition();
    }
    /// <summary>
    /// If the player's game object currently exists, set the camera's position to match that of the player's game object.
    /// </summary>
    private void SetCameraPosition()
    {
        if (PlayerExists())
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
    }

    /// <summary>
    /// Check if the player's game object exists.
    /// </summary>
    /// <returns></returns>
    private bool PlayerExists()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
