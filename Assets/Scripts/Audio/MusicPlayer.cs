using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] music;
    public bool isRocketInAtmosphere;
    AudioClip currentlyPlayingClip;
    Rocket rocket;

    void Start()
    {
        if(SceneManager.GetActiveScene().name == "FlightMode")
        {
            rocket = GameObject.FindGameObjectWithTag("Rocket").transform.GetChild(0).gameObject.GetComponent<Rocket>();
        }
        else
        {
            rocket = new Rocket
            {
                isInAtmosphere = false
            };
        }
    }

    void Update()
    {
        isRocketInAtmosphere = rocket.isInAtmosphere;
        //Debug.Log(audioSource.isPlaying);
        if(!isRocketInAtmosphere)
        {
            PlayAmbient();
        }
        else
        {
            audioSource.Stop();
        }
    }

    void PlayAmbient()
    {
        while (!audioSource.isPlaying)
        {
            while (currentlyPlayingClip == audioSource.clip)
            {
                audioSource.clip = music[Random.Range(0, music.Length)];
            }
            currentlyPlayingClip = audioSource.clip;
            audioSource.PlayDelayed(5);
        }
    }
}
