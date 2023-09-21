using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreSettings : MonoBehaviour
{
    AudioOptions audioOptions;
    GraphicOptions graphicOptions;

    void Awake()
    {
        audioOptions = GetComponent<AudioOptions>();
        graphicOptions = GetComponent<GraphicOptions>();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", audioOptions.masterVol);
        PlayerPrefs.SetFloat("MusicVolume", audioOptions.musicVol);
        PlayerPrefs.SetFloat("SFXVolume", audioOptions.sfxVol);
        PlayerPrefs.SetInt("QualityLevel", graphicOptions.qualityLevelValue);
        PlayerPrefs.SetInt("ResolutionLevel", graphicOptions.resolutionValue);

    }

    public void ResetSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", 1);
        audioOptions.masterVol = 1;
        PlayerPrefs.SetFloat("MusicVolume", 1);
        audioOptions.musicVol = 1;
        PlayerPrefs.SetFloat("SFXVolume", 1);
        audioOptions.sfxVol = 1;
        PlayerPrefs.SetInt("QualityLevel", 5);
        graphicOptions.qualityLevelValue = 5;
        PlayerPrefs.SetInt("ResolutionLevel", 3);
        graphicOptions.resolutionValue = 3;
    }
}
