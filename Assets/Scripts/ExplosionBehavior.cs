using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The sound to be played when an explosion occurs.")]
    private AudioClip explosionSound;

    [Tooltip("The game object responsible for playing sounds.")]
    private GameObject soundManagerObject;

    [Tooltip("The script that allows the sound manager game object to play sounds.")]
    private SoundManager soundManagerScript;

    [Tooltip("The animator component attached to this game obhect.")]
    private Animator animator;

    private void Start()
    {
        GetAnimator();
        FindSoundManager();
        StartCoroutine(Explode());
    }
    /// <summary>
    /// Get a reference to the animator component of this game object.
    /// </summary>
    private void GetAnimator()
    {
        animator = GetComponent<Animator>();
    }
    /// <summary>
    /// Get references to the sound manager game object and its matching script.
    /// </summary>
    private void FindSoundManager()
    {
        soundManagerObject = GameObject.FindGameObjectWithTag("Sound Manager");
        soundManagerScript = soundManagerObject.GetComponent<SoundManager>();
    }
    /// <summary>
    /// Play the explosion sound effect, then destroy the game object once the animation is complete.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Explode()
    {
        soundManagerScript.PlaySound(explosionSound, 0.5f);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
