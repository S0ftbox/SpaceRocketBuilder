using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlightModeLoader : MonoBehaviour
{
    public InputField rocketInputName;
    public SaveLoad saveLoad;
    public Button launchBtn;

    // Update is called once per frame
    void Update()
    {
        launchBtn.onClick.AddListener(() => LoadFlightScene());
    }

    void LoadFlightScene()
    {
        string rocketName = rocketInputName.text;
        if (rocketInputName.text == "")
            rocketName = "UntitledVessel";
        saveLoad.SaveRocketToFile(GameObject.Find("Rocket").transform.GetChild(0).gameObject, Application.persistentDataPath + "/SavedRockets/" + rocketName + ".bin");
        SceneManager.LoadScene("FlightMode");
    }
}
