using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DontDestroy : MonoBehaviour
{
    bool isLoaded = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "FlightMode" && !isLoaded)
        {
            this.transform.position = GameObject.Find("LaunchPlatform").transform.GetChild(0).position;
            this.transform.rotation = GameObject.Find("LaunchPlatform").transform.GetChild(0).rotation;
            this.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            isLoaded = true;
        }
    }
}
