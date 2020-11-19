using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [Tooltip("The player's game object.")]
    private GameObject player;

    private void Update()
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
