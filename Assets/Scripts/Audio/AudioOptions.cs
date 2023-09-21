using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioOptions : MonoBehaviour
{
    public float masterVol, musicVol, sfxVol;
    public Text masterVal, musicVal, sfxVal;
    public AudioMixerGroup master, music, sfx;
    public Slider masterSlider, musicSlider, sfxSlider;

    void Start()
    {
        masterVol = PlayerPrefs.GetFloat("MasterVolume");
        masterVal.text = ((int)(masterVol * 100)).ToString();
        master.audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVol) * 20);
        masterSlider.value = masterVol;
        musicVol = PlayerPrefs.GetFloat("MusicVolume");
        musicVal.text = ((int)(musicVol * 100)).ToString();
        music.audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVol) * 20);
        musicSlider.value = musicVol;
        sfxVol = PlayerPrefs.GetFloat("SFXVolume");
        sfxVal.text = ((int)(sfxVol * 100)).ToString();
        sfx.audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVol) * 20);
        sfxSlider.value = sfxVol;
        Debug.Log(QualitySettings.GetQualityLevel());
    }

    public void OnMasterSliderChange(float value)
    {
        masterVol = value;
        masterVal.text = ((int)(value * 100)).ToString();
        master.audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVol) * 20);
    }

    public void OnMusicSliderChange(float value)
    {
        musicVol = value;
        musicVal.text = ((int)(value * 100)).ToString();
        music.audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVol) * 20);
    }

    public void OnSFXSliderChange(float value)
    {
        sfxVol = value;
        sfxVal.text = ((int)(value * 100)).ToString();
        sfx.audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVol) * 20);
    }
}
