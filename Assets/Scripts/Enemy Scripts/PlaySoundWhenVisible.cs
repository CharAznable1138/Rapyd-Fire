using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundWhenVisible : MonoBehaviour
{
    [Tooltip("The AudioSource component attached to this game object.")]
    private AudioSource audioSource;

    private void Start()
    {
        GetAudioSource();
    }
    /// <summary>
    /// Get a reference to this game object's AudioSource component.
    /// </summary>
    private void GetAudioSource()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnBecameVisible()
    {
        PlaySound();
    }
    private void OnBecameInvisible()
    {
        StopSound();
    }
    /// <summary>
    /// Begin playback of sound clip.
    /// </summary>
    private void PlaySound()
    {
        audioSource.Play();
    }
    /// <summary>
    /// Cease playback of sound clip.
    /// </summary>
    private void StopSound()
    {
        audioSource.Stop();
    }
}
