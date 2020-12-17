using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayJetpackSoundWhenVisible : MonoBehaviour
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
        PlayJetpackSound();
    }
    private void OnBecameInvisible()
    {
        StopJetpackSound();
    }
    private void PlayJetpackSound()
    {
        audioSource.Play();
    }
    private void StopJetpackSound()
    {
        audioSource.Stop();
    }
}
