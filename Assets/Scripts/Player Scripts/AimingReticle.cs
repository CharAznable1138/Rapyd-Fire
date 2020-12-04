using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingReticle : MonoBehaviour
{
    [Tooltip("The PlayerController component attached to the player object.")]
    private PlayerController playerController;

    [SerializeField]
    [Tooltip("Reticle's position if player is aiming to the right. (Vector3)")]
    private Vector3 aimingRightVector;

    [SerializeField]
    [Tooltip("Reticle's position if player is aiming to the left. (Vector3)")]
    private Vector3 aimingLeftVector;

    [SerializeField]
    [Tooltip("Reticle's position if player is aiming upward. (Vector3)")]
    private Vector3 aimingUpVector;

    [SerializeField]
    [Tooltip("Reticle's position if player is aiming downward. (Vector3)")]
    private Vector3 aimingDownVector;

    [SerializeField]
    [Tooltip("Reticle's position if player is aiming upward & to the right. (Vector3)")]
    private Vector3 aimingUpRightVector;

    [SerializeField]
    [Tooltip("Reticle's position if player is aiming upward & to the left. (Vector3)")]
    private Vector3 aimingUpLeftVector;

    private void Start()
    {
        AssignPlayerController();
    }
    /// <summary>
    /// Assign a value to the PlayerController script found in this game object's parent object.
    /// </summary>
    private void AssignPlayerController()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        AlignReticleWithPlayerAimingDirection();
    }
    /// <summary>
    /// Check what direction the player is currently aiming in, then position the aiming reticle accordingly.
    /// </summary>
    private void AlignReticleWithPlayerAimingDirection()
    {
        switch (playerController.AimingDirection)
        {
            case PlayerController.AimingDirectionState.Up:
                SetLocalPosition(aimingUpVector);
                break;
            case PlayerController.AimingDirectionState.Down:
                SetLocalPosition(aimingDownVector);
                break;
            case PlayerController.AimingDirectionState.Left:
                SetLocalPosition(aimingLeftVector);
                break;
            case PlayerController.AimingDirectionState.Right:
                SetLocalPosition(aimingRightVector);
                break;
            case PlayerController.AimingDirectionState.UpLeft:
                SetLocalPosition(aimingUpLeftVector);
                break;
            case PlayerController.AimingDirectionState.UpRight:
                SetLocalPosition(aimingUpRightVector);
                break;
        }
    }

    /// <summary>
    /// Change the reticle's position, relative to that of its parent object, to a specified destination.
    /// </summary>
    /// <param name="_target">Position to move the reticle to.</param>
    private void SetLocalPosition(Vector3 _target)
    {
        transform.localPosition = _target;
    }
}
