using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    ParticleSystem flame;
    public EngineData engine;
    public GameObject rocket;

    private void Awake()
    {
        flame = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            flame.Play();
        }
        if(rocket.GetComponent<Rocket>().currentThrust == 0f)
        {
            flame.Stop();
        }
    }
}
