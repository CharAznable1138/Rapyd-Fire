using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainPanel;

    [SerializeField]
    private GameObject instructionsPanel;

    [SerializeField]
    private GameObject creditsPanel;
    private void Start()
    {
        ShowMain();
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ShowMain()
    {
        instructionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
    public void ShowInstructions()
    {
        mainPanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }
    public void ShowCredits()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
}
