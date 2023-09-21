using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicOptions : MonoBehaviour
{
    public int qualityLevelValue, resolutionValue;
    public Button qualityButtonLeft, qualityButtonRight, resolutionButtonLeft, resolutionButtonRight;
    public Text qualityLevelText, resolutionText;
    int[,] resolutionPresets = { {1280, 720}, {1366, 768}, {1600, 900}, {1920, 1080} };

    void Start()
    {
        qualityLevelValue = PlayerPrefs.GetInt("QualityLevel");
        resolutionValue = PlayerPrefs.GetInt("ResolutionLevel");
        if (qualityLevelValue == 0)
            qualityButtonLeft.interactable = false;
        if (qualityLevelValue == 5)
            qualityButtonRight.interactable = false;
        qualityLevelText.text = QualitySettings.names[qualityLevelValue];
        if (resolutionValue == 0)
            resolutionButtonLeft.interactable = false;
        if (resolutionValue == 3)
            resolutionButtonRight.interactable = false;
        resolutionText.text = resolutionPresets[resolutionValue, 0] + " x " + resolutionPresets[resolutionValue, 1];
    }

    public void OnClickLeftQuality()
    {
        if (qualityLevelValue == 5)
            qualityButtonRight.interactable = true;
        qualityLevelValue--;
        if (qualityLevelValue == 0)
            qualityButtonLeft.interactable = false;
        qualityLevelText.text = QualitySettings.names[qualityLevelValue];
        QualitySettings.DecreaseLevel();
    }

    public void OnClickRightQuality()
    {
        if (qualityLevelValue == 0)
            qualityButtonLeft.interactable = true;
        qualityLevelValue++;
        if (qualityLevelValue == 5)
            qualityButtonRight.interactable = false;
        qualityLevelText.text = QualitySettings.names[qualityLevelValue];
        QualitySettings.IncreaseLevel();
    }

    public void OnClickLeftResolution()
    {
        if (resolutionValue == 3)
            resolutionButtonRight.interactable = true;
        resolutionValue--;
        if (resolutionValue == 0)
            resolutionButtonLeft.interactable = false;
        resolutionText.text = resolutionPresets[resolutionValue, 0] + " x " + resolutionPresets[resolutionValue, 1];
        Screen.SetResolution(resolutionPresets[resolutionValue, 0], resolutionPresets[resolutionValue, 1], true);
    }

    public void OnClickRightResolution()
    {
        if (resolutionValue == 0)
            resolutionButtonLeft.interactable = true;
        resolutionValue++;
        if (resolutionValue == 3)
            resolutionButtonRight.interactable = false;
        resolutionText.text = resolutionPresets[resolutionValue, 0] + " x " + resolutionPresets[resolutionValue, 1];
        Screen.SetResolution(resolutionPresets[resolutionValue, 0], resolutionPresets[resolutionValue, 1], true);
    }
}
