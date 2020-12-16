using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Tooltip("The Audio Source component attached to this game object.")]
    private AudioSource audioSource;

    private void Start()
    {
        GetAudioSource();
    }
    /// <summary>
    /// Find the AudioSource component attached to this game object and assign it to a variable.
    /// </summary>
    private void GetAudioSource()
    {
        audioSource = GetComponent<AudioSource>();
    }
    /// <summary>
    /// Read in a sound, then play it at a normal pitch.
    /// </summary>
    /// <param name="_sound">The sound to be played.</param>
    internal void PlaySound(AudioClip _sound)
    {
        audioSource.PlayOneShot(_sound);
    }
}
