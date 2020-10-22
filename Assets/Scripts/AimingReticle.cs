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

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        if(playerController.AimingUp)
        {
            transform.localPosition = aimingUpVector;
        }
        else if(playerController.AimingDown)
        {
            transform.localPosition = aimingDownVector;
        }
        else
        {
            if(playerController.FacingRight)
            {
                transform.localPosition = aimingRightVector;
            }
            else
            {
                transform.localPosition = aimingLeftVector;
            }
        }
    }
}
