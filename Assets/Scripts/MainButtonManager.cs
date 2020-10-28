using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private GameObject spawnPoint;

    private PlayerSpawner playerSpawner;

    public void QuitToDesktop()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    public void QuitToTitle()
    {
        SceneManager.LoadScene(0);
    }
    public void Retry()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SpawnPlayer();
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void SpawnPlayer()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Spawnpoint");
        playerSpawner = spawnPoint.GetComponent<PlayerSpawner>();
        playerSpawner.SpawnPlayer();
    }
}
