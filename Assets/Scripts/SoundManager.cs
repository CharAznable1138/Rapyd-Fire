using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Tooltip("The Audio Source component attached to this game object.")]
    private AudioSource audioSource;

    [SerializeField]
    [Tooltip("The default pitch at which to play sounds.")]
    private float normalPitch = 1;

    [SerializeField]
    [Tooltip("The lowest pitch at which to play sounds.")]
    private float lowPitch = 0.5f;

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
        audioSource.pitch = normalPitch;
        audioSource.PlayOneShot(_sound);
    }
    /// <summary>
    /// Read in a sound, then play it at a low pitch.
    /// </summary>
    /// <param name="_sound">The sound to be played.</param>
    internal void PlaySoundLowPitch(AudioClip _sound)
    {
        audioSource.pitch = lowPitch;
        audioSource.PlayOneShot(_sound);
    }
}
