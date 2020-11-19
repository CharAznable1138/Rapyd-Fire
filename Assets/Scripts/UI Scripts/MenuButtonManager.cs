using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The default UI panel that the Player sees first.")]
    private GameObject mainPanel;

    [SerializeField]
    [Tooltip("The UI panel that tells the Player how to play the game.")]
    private GameObject instructionsPanel;

    [SerializeField]
    [Tooltip("The UI panel containing credits to external assets.")]
    private GameObject creditsPanel;
    private void Start()
    {
        ShowMain();
    }
    /// <summary>
    /// If game is running in Unity Editor, exit Play mode and return to Edit mode. Otherwise, close the application entirely.
    /// </summary>
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    /// <summary>
    /// Load the next scene in the Build hierarchy.
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    /// <summary>
    /// Show the default UI panel, and hide all other panels.
    /// </summary>
    public void ShowMain()
    {
        instructionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
    /// <summary>
    /// Show the Instructions UI panel, and hide all other panels.
    /// </summary>
    public void ShowInstructions()
    {
        mainPanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }
    /// <summary>
    /// Show the Credits UI panel, and hide all other panels.
    /// </summary>
    public void ShowCredits()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
}
