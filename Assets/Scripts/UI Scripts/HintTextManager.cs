using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintTextManager : MonoBehaviour
{
    [SerializeField]
    private string movementHintText = "Movement hint text will be here.";
    [SerializeField]
    private float movementHintDelay = 1;

    [SerializeField]
    private string shootHintText = "Shooting hint text will be here.";
    [SerializeField]
    private float shootHintDelay = 10;

    [SerializeField]
    private string jumpHintText = "Jumping hint text will be here.";
    [SerializeField]
    private float jumpHintDelay = 5;

    [SerializeField]
    private string lockHintText = "Movement locking hint text will be here.";
    [SerializeField]
    private float lockHintDelay = 30;

    [SerializeField]
    private string aimHintText = "Aiming hint text will be here.";
    [SerializeField]
    private float aimHintDelay = 20;

    [SerializeField]
    private float hintTime = 5;

    [SerializeField]
    private GameObject hintTextObject;

    private TMP_Text hintTextComponent;

    private GameObject player;
    private PlayerController playerController;

    private bool showHintCoroutineIsRunning;

    private List<Hint> hints = new List<Hint>();

    private List<Hint> noShowHints = new List<Hint>();

    private void Start()
    {
        showHintCoroutineIsRunning = false;
        hintTextComponent = hintTextObject.GetComponent<TMP_Text>();
        hintTextObject.SetActive(false);
        hints.Add(new Hint(Hint.HintType.Move, movementHintText, movementHintDelay));
        hints.Add(new Hint(Hint.HintType.Shoot, shootHintText, shootHintDelay));
        hints.Add(new Hint(Hint.HintType.Jump, jumpHintText, jumpHintDelay));
        hints.Add(new Hint(Hint.HintType.Lock, lockHintText, lockHintDelay));
        hints.Add(new Hint(Hint.HintType.Aim, aimHintText, aimHintDelay));
    }
    private void Update()
    {
        if(PlayerExists() && !showHintCoroutineIsRunning)
        {
            StopAllCoroutines();
            StartCoroutine(ShowHint());
        }
    }
    private IEnumerator ShowHint()
    {
        showHintCoroutineIsRunning = true;
        foreach(Hint h in hints)
        {
            switch(h.hintType)
            {
                case Hint.HintType.Move:
                    if(playerController.HasMoved)
                    {
                        noShowHints.Add(h);
                    }
                    break;
                case Hint.HintType.Jump:
                    if(playerController.HasJumped)
                    {
                        noShowHints.Add(h);
                    }
                    break;
                case Hint.HintType.Shoot:
                    if (playerController.HasShot)
                    {
                        noShowHints.Add(h);
                    }
                    break;
                case Hint.HintType.Aim:
                    if (playerController.HasAimed)
                    {
                        noShowHints.Add(h);
                    }
                    break;
                case Hint.HintType.Lock:
                    if (playerController.HasLocked)
                    {
                        noShowHints.Add(h);
                    }
                    break;
            }
            if(Time.timeSinceLevelLoad > h.hintDelay && !h.playerHasAlreadySeen && !noShowHints.Contains(h))
            {
                hintTextComponent.text = h.hintText;
                hintTextObject.SetActive(true);
                h.playerHasAlreadySeen = true;
                yield return new WaitForSeconds(hintTime);
                hintTextObject.SetActive(false);
            }
        }
        showHintCoroutineIsRunning = false;
        yield break;
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
}
