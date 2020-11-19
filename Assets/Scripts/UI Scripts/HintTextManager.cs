using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintTextManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text to display for movement hints. (String)")]
    private string movementHintText = "Movement hint text will be here.";
    [SerializeField]
    [Tooltip("The amount of time to wait before displaying movement hints. (Float)")]
    private float movementHintDelay = 1;

    [SerializeField]
    [Tooltip("The text to display for shooting hints. (String)")]
    private string shootHintText = "Shooting hint text will be here.";
    [SerializeField]
    [Tooltip("The amount of time to wait before displaying shooting hints. (Float)")]
    private float shootHintDelay = 10;

    [SerializeField]
    [Tooltip("The text to display for jumping hints. (String)")]
    private string jumpHintText = "Jumping hint text will be here.";
    [SerializeField]
    [Tooltip("The amount of time to wait before displaying jumping hints. (Float)")]
    private float jumpHintDelay = 5;

    [SerializeField]
    [Tooltip("The text to display for movement-locking hints. (String)")]
    private string lockHintText = "Movement locking hint text will be here.";
    [SerializeField]
    [Tooltip("The amount of time to wait before displaying movement-locking hints. (Float)")]
    private float lockHintDelay = 30;

    [SerializeField]
    [Tooltip("The text to display for aiming hints. (String)")]
    private string aimHintText = "Aiming hint text will be here.";
    [SerializeField]
    [Tooltip("The amount of time to wait before displaying aiming hints. (Float)")]
    private float aimHintDelay = 20;

    [SerializeField]
    [Tooltip("The amount of time to show hints. (Float)")]
    private float hintTime = 5;

    [SerializeField]
    [Tooltip("The UI object that displays hint text.")]
    private GameObject hintTextObject;

    [Tooltip("The TextMeshPro Text component attached to the Hint Text UI object.")]
    private TMP_Text hintTextComponent;

    [Tooltip("The Player's game object.")]
    private GameObject player;
    [Tooltip("The PlayerController component attached to the Player's game object.")]
    private PlayerController playerController;

    [Tooltip("True = Show Hint coroutine is currently running, False = Show Hint coroutine is not currently running. (Bool)")]
    private bool showHintCoroutineIsRunning;

    [Tooltip("List of hints to show to the Player.")]
    private List<Hint> hints = new List<Hint>();

    [Tooltip("List of hints the Player has already seen, and does not need to see again.")]
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
    /// <summary>
    /// Show the player a hint, and add said hint to the list of hints the player has already seen.
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Check if the Player's game object exists. True = yes, False = no.
    /// </summary>
    /// <returns></returns>
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
