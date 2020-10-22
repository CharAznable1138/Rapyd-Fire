using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingReticle : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField]
    private Vector3 aimingRightVector;

    [SerializeField]
    private Vector3 aimingLeftVector;

    [SerializeField]
    private Vector3 aimingUpVector;

    [SerializeField]
    private Vector3 aimingDownVector;

    [SerializeField]
    private Vector3 aimingUpRightVector;

    [SerializeField]
    private Vector3 aimingUpLeftVector;

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        switch(playerController.AimingDirection)
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
    private void SetLocalPosition(Vector3 _target)
    {
        transform.localPosition = _target;
    }
}
