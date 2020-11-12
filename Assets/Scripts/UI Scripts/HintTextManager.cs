using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintTextManager : MonoBehaviour
{
    [SerializeField]
    private string movementHint = "Movement hint text will be here.";
    [SerializeField]
    private float movementHintDelay = 1;

    [SerializeField]
    private string shootHint = "Shooting hint text will be here.";
    [SerializeField]
    private float shootHintDelay = 10;

    [SerializeField]
    private string jumpHint = "Jumping hint text will be here.";
    [SerializeField]
    private float jumpHintDelay = 5;

    [SerializeField]
    private string lockHint = "Movement locking hint text will be here.";
    [SerializeField]
    private float lockHintDelay = 30;

    [SerializeField]
    private string aimHint = "Aiming hint text will be here.";
    [SerializeField]
    private float aimHintDelay = 20;

    [SerializeField]
    private float hintTime = 5;

    [SerializeField]
    private GameObject hintTextObject;

    private TMP_Text hintTextComponent;

    private GameObject player;
    private PlayerController playerController;

    private bool playerHasSeenMoveHint = false;
    private bool playerHasSeenShootHint = false;
    private bool playerHasSeenJumpHint = false;
    private bool playerHasSeenLockHint = false;
    private bool playerHasSeenAimHint = false;
    private enum HintType
    {
        Move,
        Shoot,
        Jump,
        Lock,
        Aim
    }
    private HintType hintToShow;

    private void Start()
    {
        playerHasSeenMoveHint = false;
        playerHasSeenShootHint = false;
        playerHasSeenJumpHint = false;
        playerHasSeenLockHint = false;
        playerHasSeenAimHint = false;
        hintTextComponent = hintTextObject.GetComponent<TMP_Text>();
        hintTextObject.SetActive(false);
    }
    private void Update()
    {
        if(PlayerExists())
        {
            if(!playerController.HasMoved && Time.timeSinceLevelLoad > movementHintDelay && !playerHasSeenMoveHint)
            {
                hintToShow = HintType.Move;
                StopAllCoroutines();
                StartCoroutine("ShowHint");
                playerHasSeenMoveHint = true;
                //Debug.Log("Player hasn't tried moving yet.");
            }
            if (!playerController.HasJumped && Time.timeSinceLevelLoad > jumpHintDelay && !playerHasSeenJumpHint)
            {
                hintToShow = HintType.Jump;
                StopAllCoroutines();
                StartCoroutine("ShowHint");
                playerHasSeenJumpHint = true;
                //Debug.Log("Player hasn't tried jumping yet.");
            }
            if (!playerController.HasShot && Time.timeSinceLevelLoad > shootHintDelay && !playerHasSeenShootHint)
            {
                hintToShow = HintType.Shoot;
                StopAllCoroutines();
                StartCoroutine("ShowHint");
                playerHasSeenShootHint = true;
                //Debug.Log("Player hasn't tried shooting yet.");
            }
            if (!playerController.HasAimed && Time.timeSinceLevelLoad > aimHintDelay && !playerHasSeenAimHint)
            {
                hintToShow = HintType.Aim;
                StopAllCoroutines();
                StartCoroutine("ShowHint");
                playerHasSeenAimHint = true;
                //Debug.Log("Player hasn't tried aiming yet.");
            }
            if (!playerController.HasLocked && Time.timeSinceLevelLoad > lockHintDelay && !playerHasSeenLockHint)
            {
                hintToShow = HintType.Lock;
                StopAllCoroutines();
                StartCoroutine("ShowHint");
                playerHasSeenLockHint = true;
                //Debug.Log("Player hasn't tried locking yet.");
            }
        }
    }
    private bool PlayerExists()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            playerController = player.GetComponent<PlayerController>();
            return true;
        }
        else
        {
            return false;
        }
    }
    private IEnumerator ShowHint()
    {
        hintTextObject.SetActive(true);
        switch(hintToShow)
        {
            case HintType.Move:
                SetHintText(movementHint);
                break;
            case HintType.Jump:
                SetHintText(jumpHint);
                break;
            case HintType.Shoot:
                SetHintText(shootHint);
                break;
            case HintType.Lock:
                SetHintText(lockHint);
                break;
            case HintType.Aim:
                SetHintText(aimHint);
                break;
        }
        yield return new WaitForSeconds(hintTime);
        hintTextObject.SetActive(false);
    }
    private void SetHintText(string hint)
    {
        hintTextComponent.text = hint;
    }
}
