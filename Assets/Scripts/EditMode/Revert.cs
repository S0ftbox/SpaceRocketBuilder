using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Revert : MonoBehaviour, IPointerDownHandler
{
    public GameObject loadingScreen, loadingIndicator, loadingImage, parent;
    public Texture[] textures;
    public string name;
    int percent;

    void StartGame(string sceneName)
    {
        if (parent.transform.childCount > 0)
        {
            DestroyImmediate(parent.transform.GetChild(0).gameObject);
        }
        AsyncLoadScene(sceneName);
    }

    public async void AsyncLoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        loadingImage.GetComponent<RawImage>().texture = textures[Random.Range(0, 2)];
        loadingScreen.SetActive(true);

        do
        {
            await Task.Delay(1);
            if (loadingIndicator != null)
            {
                percent = (int)(scene.progress * 100);
                loadingIndicator.GetComponent<Text>().text = percent.ToString() + "%";
            }
        } while (scene.progress < 0.9f);

        if (loadingScreen.gameObject != null)
        {
            loadingScreen.SetActive(false);
        }
        scene.allowSceneActivation = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartGame(name);
    }
}
