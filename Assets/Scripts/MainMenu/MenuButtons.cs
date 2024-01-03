using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public Button options, credits, quit;
    public GameObject optionsPanel, creditsPanel;

    void Update()
    {
        options.onClick.AddListener(() => ShowOptions());
        credits.onClick.AddListener(() => ShowCredits());
        quit.onClick.AddListener(() => QuitGame());
    }

    void ShowOptions()
    {
        optionsPanel.SetActive(true);
    }

    void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
