using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class MenuButtons : MonoBehaviour
{
    public Button play, options, credits, quit;
    public GameObject optionsPanel, creditsPanel, loadingScreen, loadingIndicator;

    void Update()
    {
        play.onClick.AddListener(() => StartGame());
        options.onClick.AddListener(() => ShowOptions());
        credits.onClick.AddListener(() => ShowCredits());
        quit.onClick.AddListener(() => QuitGame());
    }

    void StartGame()
    {
        SceneManager.LoadScene("SpaceCenter");
        //AsyncLoadScene("SpaceCenter");
    }

    public async void AsyncLoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loadingScreen.SetActive(true);

        do
        {
            await Task.Delay(1);
            if(loadingIndicator != null)
            {
                loadingIndicator.GetComponent<Text>().text = scene.progress.ToString();
            }
        } while (scene.progress < 0.9f);

        loadingScreen.SetActive(false);
        scene.allowSceneActivation = true;
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
