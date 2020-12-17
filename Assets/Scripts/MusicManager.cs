using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("A list of music tracks to use in the game. For best results, arrange in the following order: Main Menu, Level 1, Level 2, Level 3, Final Results")]
    private AudioClip[] musicTracks;

    [Tooltip("The Audio Source component attached to this game object.")]
    private AudioSource audioSource;

    private void Start()
    {
        GetAudioSource();
        SetLevelMusic();
    }
    /// <summary>
    /// Find the AudioSource component attached to this game object and assign it to a variable.
    /// </summary>
    private void GetAudioSource()
    {
        audioSource = GetComponent<AudioSource>();
    }
    /// <summary>
    /// Read in a music track, then play it at the specified volume.
    /// </summary>
    /// <param name="_sound">The music to be played.</param>
    /// <param name="_volume">The volume at which to play the music (min. 0.0, max. 1.0)</param>
    internal void PlayMusic(AudioClip _music, float _volume)
    {
        audioSource.volume = _volume;
        audioSource.clip = _music;
        audioSource.Play();
    }
    /// <summary>
    /// Stop playing the current music track.
    /// </summary>
    internal void StopMusic()
    {
        audioSource.Stop();
    }
    /// <summary>
    /// Check which music is currently playing.
    /// </summary>
    /// <returns></returns>
    internal AudioClip CurrentMusic()
    {
        return audioSource.clip;
    }
    /// <summary>
    /// Check the build index of the current scene, then play its corresponding music track.
    /// </summary>
    private void SetLevelMusic()
    {
        audioSource.clip = musicTracks[SceneManager.GetActiveScene().buildIndex];
        audioSource.Play();
    }
    /// <summary>
    /// Pause the current music track.
    /// </summary>
    internal void PauseMusic()
    {
        audioSource.Pause();
    }
    /// <summary>
    /// Play whatever music track is currently loaded into the music manager's AudioSource.
    /// </summary>
    internal void PlayCurrentMusic()
    {
        audioSource.Play();
    }
}
