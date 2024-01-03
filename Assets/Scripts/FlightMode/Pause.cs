using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public bool isPaused = false;
    public TimeScale timeScale;
    public GameObject pauseMenu, options;
    public Button optionsBtn, quitBtn;

    void Update()
    {
        optionsBtn.onClick.AddListener(() => ShowOptions());
        quitBtn.onClick.AddListener(() => QuitGame());

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                pauseMenu.SetActive(true);
                isPaused = true;
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.SetActive(false);
                isPaused = false;
                Time.timeScale = timeScale.timeScaleMultiplier;
            }
        }
    }

    void ShowOptions()
    {
        options.SetActive(true);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
