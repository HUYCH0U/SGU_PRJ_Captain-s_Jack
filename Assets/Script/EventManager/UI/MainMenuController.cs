using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private Button StartButton;
    private Button QuitButton;
    void Awake()
    {
        StartButton = transform.Find("PlayButton").GetComponent<Button>();
        QuitButton = transform.Find("QuitButton").GetComponent<Button>();
        StartButton.onClick.AddListener(StartGame);
        QuitButton.onClick.AddListener(Quit);
    }

    private void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    private void Quit()
    {
        Application.Quit();
    }
}
