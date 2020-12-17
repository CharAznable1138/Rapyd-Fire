using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundWhenVisible : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        GetAudioSource();
    }
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
    private void PlaySound()
    {
        audioSource.Play();
    }
    private void StopSound()
    {
        audioSource.Stop();
    }
}
